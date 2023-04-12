using Authentication.Core.Models;
using Authentication.Services.Users;
using Authentication.Services.Users.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationWebapi.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("User")]
        public async Task<ActionResult<User>> CreateUser([FromBody] RegistrationDTO request)
        {
            var dbUser = await _userService.CreateUserAsync(request);
            if (dbUser != null && string.IsNullOrEmpty(dbUser.Id) && !string.IsNullOrEmpty(dbUser.Username))
            {
                return BadRequest(dbUser.Username);
            }

            return Ok(dbUser);
        }

        [HttpPut("User-update")]
        public async Task<ActionResult<User>> UpdateUser([FromBody] RegistrationDTO request)
        {
            var dbUser = await _userService.UpdateUserAsync(request);
            if (dbUser != null && string.IsNullOrEmpty(dbUser.Id) && !string.IsNullOrEmpty(dbUser.Username))
            {
                return BadRequest(dbUser.Username);
            }

            return Ok(dbUser);
        }

        [HttpGet("User-list")]
        public async Task<ActionResult<User>> UserList()
        {
            var dbUserList = await _userService.UserListAsync();
            return Ok(dbUserList);
        }

        [HttpGet("User/{id}")]
        public async Task<ActionResult<User>> UserById(string id)
        {
            var dbUser = await _userService.UserByIdAsync(id);
            return Ok(dbUser);
        } 

        [HttpDelete("User-delete/{id}")]
        public async Task<ActionResult<User>> UserDeleteById(string id)
        {
            var dbUser = await _userService.UserDeleteByIdAsync(id);
            return Ok(dbUser);
        }
    }
}
