using Authentication.Core.Models;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using static System.Net.Mime.MediaTypeNames;
using Authentication.Services.SiteInformations;
using Authentication.Services.SiteInformations.Request;
using Authentication.Services.MineInformations;
using Authentication.Services.MineInformations.Request;

namespace AuthenticationWebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MineInformationController : ControllerBase
    {
        private readonly IMineInformationService _service;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnv;

        public MineInformationController(IMineInformationService service, IWebHostEnvironment env, IConfiguration configuration)
        {
            _service = service;
            _hostingEnv = env;
            _configuration = configuration;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<MineInformation>> Create([FromBody] MineInformationsDTO request)
        {
            var mineInformation = await _service.CreateAsync(request);
            if(mineInformation.Code == 200)
            {
                return Ok(mineInformation);
            }
           return BadRequest(mineInformation.Message);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<MineInformation>> Update([FromBody] MineInformationsDTO request)
        {
            var mineInformation = await _service.UpdateAsync(request);
            if (mineInformation.Code == 200)
            {
                return Ok(mineInformation);
            }
            return BadRequest(mineInformation.Message);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<MineInformation>> GetAll()
        {
            var mineInformationList = await _service.ListAsync();
            return Ok(mineInformationList);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<MineInformation>> GetById(string id)
        {
            var mineInformation = await _service.GetByIdAsync(id);
            return Ok(mineInformation);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<MineInformation>> DeleteById(string id)
        {
            var mineInformation = await _service.DeleteByIdAsync(id);
            return Ok(mineInformation);
        }
    }
}
