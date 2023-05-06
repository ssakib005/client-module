using Authentication.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Authentication.Services.MCPLinks;
using Authentication.Services.MCPLinks.Request;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using static System.Net.Mime.MediaTypeNames;

namespace AuthenticationWebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MCPLinkController : ControllerBase
    {
        private readonly IMCPLinkService _service;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnv;

        public MCPLinkController(IMCPLinkService service, IWebHostEnvironment env, IConfiguration configuration)
        {
            _service = service;
            _hostingEnv = env;
            _configuration = configuration;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<MCPLink>> Create([FromBody] MCPLinkDTO request)
        {
            var mcpLink = await _service.CreateAsync(request);
            if(mcpLink.Code == 200)
            {
                return Ok(mcpLink);
            }
           return BadRequest(mcpLink.Message);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<MCPLink>> Update([FromBody] MCPLinkDTO request)
        {
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
    }
}
