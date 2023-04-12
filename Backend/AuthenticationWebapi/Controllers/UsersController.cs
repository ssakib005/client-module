using Authentication.Core.Interfaces;
using Authentication.Core.Models;
using Authentication.Services.Users;
using Authentication.Services.Users.Request;
using Authentication.Services.Users.Response;
using AuthenticationWebapi.Authentication;
using AuthenticationWebapi.WebModels;
using DnsClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationWebapi.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ISessionService _sessionService;
        private readonly IJwtTokenService _jwtTokenService;
        public UsersController(IUserService userService, IJwtTokenService jwtTokenService
            , ISessionService sessionService)
        {
            _userService = userService;
            _jwtTokenService = jwtTokenService;
            _sessionService = sessionService;
        }

        //[AllowAnonymous]
        //[HttpPost("User")]
        //public async Task<ActionResult<User>> CreateUser([FromBody] UserRequest request)
        //{
        //    var dbUser = await _userService.CreateUserAsync(request);
        //    if (dbUser != null && string.IsNullOrEmpty(dbUser.Id) && !string.IsNullOrEmpty(dbUser.Username))
        //    {
        //        return BadRequest(dbUser.Username);
        //    }

        //    return Ok(dbUser);
        //}

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] UserRequest request)
        {
            var dbUser = await _userService.GetUserAsync(request);
            if (dbUser == null)
            {
                return BadRequest("Invalid username/password");
            }

            var response = new LoginResponse
            {
                Id = dbUser.Id
            };

            var token = _jwtTokenService.GenerateTokens(dbUser);
            if (token != null)
            {
                response.AccessToken = token.AccessToken;
                response.RefreshToken = token.RefreshToken;
            }

            return Ok(response);
        }


        [AllowAnonymous]
        [HttpPost("RefreshToken")]
        public async Task<ActionResult<JwtToken>> RefreshToken([FromBody] TokenRequest request)
        {
            var token = await _jwtTokenService.RefreshUserTokenAsync(request);
            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        [Authorize]
        [HttpPut("ProfileDetails")]
        public async Task<ActionResult<CommonResponse>> SaveProfileDetails([FromForm] SaveProfileRequest request)
        {
            var user = new User {Id = _sessionService.GetUserId(), FirstName = request.FirstName, LastName = request.LastName, Email = request.Email};
            if (request.LogoBits != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await request.LogoBits.CopyToAsync(memoryStream);
                    user.LogoBits = memoryStream.ToArray();
                }
            }

            var response = await _userService.SaveProfileDetails(user);
            return Ok(response);
        }



        [Authorize]
        [HttpGet("ProfileDetails")]
        public async Task<ActionResult<User>> SaveProfileDetails()
        {
            var user = await _userService.GetCurrentUserProfileDetails();
            return Ok(user);
        }
    }
}
