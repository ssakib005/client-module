using Authentication.Core.Models;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using static System.Net.Mime.MediaTypeNames;
using Authentication.Services.SiteInformations;
using Authentication.Services.SiteInformations.Request;
using Authentication.Services.MineInformations;
using Authentication.Services.SiteInformations.Response;

namespace AuthenticationWebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteInformationController : ControllerBase
    {
        private readonly ISiteInformationService _service;
        private readonly IMineInformationService _mineInformationService;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnv;

        public SiteInformationController(ISiteInformationService service, IWebHostEnvironment env,
            IConfiguration configuration, IMineInformationService mineInformationService)
        {
            _service = service;
            _hostingEnv = env;
            _configuration = configuration;
            _mineInformationService = mineInformationService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<User>> Create([FromBody] SiteInformationsDTO request)
        {
            var siteInformation = await _service.CreateAsync(request);
            if(siteInformation.Code == 200)
            {
                return Ok(siteInformation);
            }
           return BadRequest(siteInformation.Message);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<User>> Update([FromBody] SiteInformationsDTO request)
        {
            var siteInformation = await _service.UpdateAsync(request);
            if (siteInformation.Code == 200)
            {
                return Ok(siteInformation);
            }
            return BadRequest(siteInformation.Message);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<User>> GetAll()
        {
            var siteInformationList = await _service.ListAsync();
            return Ok(siteInformationList);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<User>> GetById(string id)
        {
            var siteInformation = await _service.GetByIdAsync(id);
            return Ok(siteInformation);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<User>> DeleteById(string id)
        {
            var siteInformation = await _service.DeleteByIdAsync(id);
            return Ok(siteInformation);
        }

        [HttpGet("GetListByMineInformationId/{id}")]
        public async Task<ActionResult<SiteInformation>> GetListByMineInformationId(string id)
        {
            var mineInformation = await _mineInformationService.GetByIdAsync(id);
            var siteInformationList = new List<SiteInformationsList>();
            foreach(var item in mineInformation.Data.SiteInformationList) 
            {
                var siteInformation = await _service.GetByIdAsync(item);
                if (siteInformation != null)
                    siteInformationList.Add(siteInformation.Data);
            }
            return Ok(siteInformationList);
        }
    }
}
