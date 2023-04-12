using Authentication.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Authentication.Services.FunctionalLocations;
using Authentication.Services.FunctionalLocations.Request;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using static System.Net.Mime.MediaTypeNames;

namespace AuthenticationWebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FunctionalLocationController : ControllerBase
    {
        private readonly IFunctionalLocationService _service;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnv;

        public FunctionalLocationController(IFunctionalLocationService service, IWebHostEnvironment env, IConfiguration configuration)
        {
            _service = service;
            _hostingEnv = env;
            _configuration = configuration;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<User>> Create([FromBody] FunctionalLocationDTO request)
        {
            var location = await _service.CreateAsync(request);
            if(location.Code == 200)
            {
                return Ok(location);
            }
           return BadRequest(location.Message);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<User>> Update([FromBody] FunctionalLocationDTO request)
        {
            var location = await _service.UpdateAsync(request);
            if (location.Code == 200)
            {
                return Ok(location);
            }
            return BadRequest(location.Message);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<User>> GetAll()
        {
            var dbUserList = await _service.ListAsync();
            return Ok(dbUserList);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<User>> GetById(string id)
        {
            var dbUser = await _service.GetByIdAsync(id);
            return Ok(dbUser);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<User>> DeleteById(string id)
        {
            var dbUser = await _service.DeleteByIdAsync(id);
            return Ok(dbUser);
        }
    }
}
