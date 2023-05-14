using Authentication.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Authentication.Services.MCPLinks;
using Authentication.Services.MCPLinks.Request;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using static System.Net.Mime.MediaTypeNames;
using Authentication.Services.MineInformations;
using Authentication.Services.SiteInformations;
using Authentication.Services.MCPBoards;
using Authentication.Services.FunctionalLocations;
using Authentication.Services.MCPLinks.Response;

namespace AuthenticationWebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MCPLinkController : ControllerBase
    {
        private readonly IMCPLinkService _service;
        private readonly IMineInformationService _mineInformationService;
        private readonly ISiteInformationService _siteInformationService;
        private readonly IMCPBoardService _mcpBoardService;
        private readonly IFunctionalLocationService _functionalLocationService;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnv;

        public MCPLinkController(IMCPLinkService service, IWebHostEnvironment env,
            IConfiguration configuration, IMineInformationService mineInformationService,
            ISiteInformationService siteInformationService, IMCPBoardService mcpBoardService,
            IFunctionalLocationService functionalLocationService)
        {
            _service = service;
            _hostingEnv = env;
            _configuration = configuration;
            _mineInformationService = mineInformationService;
            _siteInformationService = siteInformationService;
            _mcpBoardService= mcpBoardService;
            _functionalLocationService = functionalLocationService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<MCPLink>> Create([FromBody] List<MCPLinkList> request)
        {

            try
            {
                var existingMCPLinkList = await _service.GetListByMineAndSiteInformationId(request[0].MineInformationId, request[0].SiteInformationId);
                foreach(var ex in existingMCPLinkList.Data) 
                {
                    await _service.DeleteByIdAsync(ex.Id);
                }

                foreach (var item in request)
                {
                    var link = new MCPLinkDTO();
                    link.MineInformationId = item.MineInformationId;
                    link.SiteInformationId = item.SiteInformationId;
                    link.MCPBoardId = item.MCPBoardId;
                    link.Panel = item.Panel;
                    link.FunctionalLocationId = item.FunctionalLocationId;
                    link.Link = item.Link;

                    await _service.CreateAsync(link);

                }
                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest();
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult<MCPLink>> Update([FromBody] MCPLinkDTO request)
        {
            var mineInformation = await _mineInformationService.GetByIdAsync(request.MineInformationId);
            var siteInformation = await _siteInformationService.GetByIdAsync(request.SiteInformationId);
            var mcpBoard = await _mcpBoardService.GetByIdAsync(request.MCPBoardId);
            var functionalLocation = await _functionalLocationService.GetByIdAsync(request.FunctionalLocationId);

            request.Link = mineInformation.Data.Name + " " + siteInformation.Data.Name + " " + mcpBoard.Data.Name + " " + request.Panel + " " + functionalLocation.Data.Name;

            var mcpLink = await _service.UpdateAsync(request);
            if (mcpLink.Code == 200)
            {
                return Ok(mcpLink);
            }
            return BadRequest(mcpLink.Message);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<MCPLink>> GetAll()
        {
            var mcpLinkList = await _service.ListAsync();
            return Ok(mcpLinkList);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<MCPLink>> GetById(string id)
        {
            var mcpLink = await _service.GetByIdAsync(id);
            return Ok(mcpLink);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<MCPLink>> DeleteById(string id)
        {
            var mcpLink = await _service.DeleteByIdAsync(id);
            return Ok(mcpLink);
        }

        [HttpGet("GetListByMineAndSiteInformationId/{mid}/{sid}")]
        public async Task<ActionResult<MCPLink>> GetListByMineAndSiteInformationId(string mid, string sid)
        {
            var mcpLinkList = await _service.GetListByMineAndSiteInformationId(mid,sid);
            return Ok(mcpLinkList);
        }
    }
}
