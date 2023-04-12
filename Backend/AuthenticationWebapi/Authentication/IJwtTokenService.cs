using Authentication.Core.Models;
using Authentication.Services.Users.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationWebapi.Authentication
{
    public interface IJwtTokenService
    {
        JwtToken GenerateTokens(User user);
        Task<JwtToken> RefreshUserTokenAsync(TokenRequest request);
        Task RevokeRefreshTokenAsync(TokenRequest request);
        string GeneratePasswordResetToken();
    }
}
