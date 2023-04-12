using Authentication.Core.Models;
using Authentication.Services.Users.Request;
using Authentication.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Authentication.Services.FunctionalLocation.Request;
using Authentication.Services.FunctionalLocation;

namespace AuthenticationWebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FunctionalLocationController : ControllerBase
    {
        private readonly IFunctionalLocation _service;
        public FunctionalLocationController(IFunctionalLocation service)
        {
            _service = service;
        }

        //[HttpPost("Create")]
        //public async Task<ActionResult<User>> Create([FromBody] FunctionalLocationDTO request)
        //{
        //    //var dbUser = await _service.CreateUserAsync(request);
        //    //if (dbUser != null && string.IsNullOrEmpty(dbUser.Id) && !string.IsNullOrEmpty(dbUser.Username))
        //    //{
        //    //    return BadRequest(dbUser.Username);
        //    //}

        //    return Ok(dbUser);
        //}

        //[HttpPut("Update")]
        //public async Task<ActionResult<User>> Update([FromBody] RegistrationDTO request)
        //{
        //    var dbUser = await _service.UpdateUserAsync(request);
        //    if (dbUser != null && string.IsNullOrEmpty(dbUser.Id) && !string.IsNullOrEmpty(dbUser.Username))
        //    {
        //        return BadRequest(dbUser.Username);
        //    }

        //    return Ok(dbUser);
        //}

        //[HttpGet("GetAll")]
        //public async Task<ActionResult<User>> GetAll()
        //{
        //    var dbUserList = await _service.UserListAsync();
        //    return Ok(dbUserList);
        //}

        //[HttpGet("GetById/{id}")]
        //public async Task<ActionResult<User>> GetById(string id)
        //{
        //    var dbUser = await _service.UserByIdAsync(id);
        //    return Ok(dbUser);
        //}

        //[HttpDelete("Delete/{id}")]
        //public async Task<ActionResult<User>> DeleteById(string id)
        //{
        //    var dbUser = await _service.UserDeleteByIdAsync(id);
        //    return Ok(dbUser);
        //}
    }
}
