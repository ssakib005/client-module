using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Authentication.Core.MongoDb.Repository;
using Authentication.Core.Options;
using Authentication.Core.Models;
using Authentication.Services.Users.Request;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationWebapi.Authentication
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly ApiConfigurationOptions _apiConfig;
           private readonly IMongoDbRepository<RefreshToken> _refreshTokenRepository;
        private readonly IMongoDbRepository<User> _userRepository;

        public JwtTokenService(IOptions<ApiConfigurationOptions> apiConfig,  IMongoDbRepository<RefreshToken> refreshTokenRepository, IMongoDbRepository<User> userRepository)
        {
            _apiConfig = apiConfig.Value;
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
        }

        public JwtToken GenerateTokens(User user)
        {
            var jwtToken = new JwtToken()
            {
                AccessToken = CreateAccessToken(user),
                RefreshToken = GenerateRefreshTokenString()
            };

            var refreshToken = new RefreshToken
            {
                UserId = user.Id,
                TokenString = jwtToken.RefreshToken,
                ExpiredOn = DateTime.Now.AddMinutes(_apiConfig.JwtToken.RefreshTokenExpirationMin)
            };

            _refreshTokenRepository.Insert(refreshToken);
            return jwtToken;
        }

        private static string GenerateRefreshTokenString()
        {
            var randomNumber = new byte[32];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private string CreateAccessToken(User user)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var secret = GetSecurityKey();
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private SymmetricSecurityKey GetSecurityKey()
        {
            var key = Encoding.UTF8.GetBytes(_apiConfig.JwtToken.Secret);
            return new SymmetricSecurityKey(key);
        }

        private List<Claim> GetClaims(User user)
        {
            var claims = new[]
            {
                new Claim(CustomClaimTypes.Email,user.Email),
                new Claim(CustomClaimTypes.UserId,user.Id),
            };

            return claims.ToList();
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken
            (
                issuer: _apiConfig.JwtToken.Issuer,
                audience: _apiConfig.JwtToken.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_apiConfig.JwtToken.AccessTokenExpirationMin),
                signingCredentials: signingCredentials
            );

            return tokenOptions;
        }

        public async Task<JwtToken> RefreshUserTokenAsync(TokenRequest request)
        {
            var tokenPrincipal = GetTokenPrincipal(request.AccessToken);
            if (tokenPrincipal == null || tokenPrincipal.Claims == null)
                return null;

            string userId = tokenPrincipal?.Claims?.Where(x => x.Type == CustomClaimTypes.UserId).FirstOrDefault()?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                var refrshToken = await _refreshTokenRepository.FindOneAsync(t => t.UserId == userId && t.TokenString.Equals(request.RefreshToken));
                if (refrshToken != null)
                {
                    if (DateTime.UtcNow < refrshToken.ExpiredOn && DateTime.UtcNow < refrshToken.CreatedAt.AddDays(_apiConfig.JwtToken.RefreshTokenMaximumLifeSpanDays))
                    {
                        refrshToken.ExpiredOn = DateTime.Now.AddMinutes(_apiConfig.JwtToken.RefreshTokenExpirationMin);
                        await _refreshTokenRepository.UpdateAsync(refrshToken);
                        User user = await _userRepository.GetByIdAsync(userId);
                        return new JwtToken
                        {
                            AccessToken = CreateAccessToken(user),
                            RefreshToken = request.RefreshToken
                        };
                    }
                    else
                    {
                        await _refreshTokenRepository.DeleteAsync(refrshToken);
                    }
                }
            }
            return null;
        }


        private ClaimsPrincipal GetTokenPrincipal(string accessToken)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = GetSecurityKey(),
                ValidateLifetime = false
            };
            SecurityToken securityToken;
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                return null;

            return principal;
        }

        public async Task RevokeRefreshTokenAsync(TokenRequest request)
        {

            var tokenPrincipal = GetTokenPrincipal(request.AccessToken);
            if (tokenPrincipal == null || tokenPrincipal.Claims == null)
                return;

            string userId = tokenPrincipal.Claims.Where(x => x.Type == CustomClaimTypes.UserId).FirstOrDefault()?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                var refrshToken = await _refreshTokenRepository.FindOneAsync(t => t.UserId == userId && t.TokenString.Equals(request.RefreshToken));
                if (refrshToken != null)
                {
                    await _refreshTokenRepository.DeleteAsync(refrshToken);
                }
            }
        }

        public string GeneratePasswordResetToken()
        {
            string randomToken = GenerateRefreshTokenString();
            var tokenByes = Encoding.UTF8.GetBytes(randomToken);
            var urlEncodedToken = WebEncoders.Base64UrlEncode(tokenByes);
            return urlEncodedToken;
        }
    }
}
