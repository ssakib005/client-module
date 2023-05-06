using Authentication.Core.Interfaces;
using Authentication.Core.MongoDb.Repository;
using Authentication.Core.Options;
using Microsoft.Extensions.Options;
using Authentication.Core.Models;
using Authentication.Services.MCPLinks.Request;
using Authentication.Services.MCPLinks.Response;

namespace Authentication.Services.MCPLinks
{
    public class MCPLinkService : IMCPLinkService
    {
        private readonly ApiConfigurationOptions _apiConfig;
        private readonly IMongoDbRepository<MCPLink> _mcpLinkRepository;
        private readonly ISessionService _sessionService;
        public MCPLinkService(
            IOptions<ApiConfigurationOptions> apiConfig,
            IMongoDbRepository<MCPLink> mcpLinkRepository,
            ISessionService sessionService)
        {
            _apiConfig = apiConfig.Value;
            _mcpLinkRepository = mcpLinkRepository;
            _sessionService = sessionService;
        }


        public async Task<MCPLinkResponse<bool>> CreateAsync(MCPLinkDTO request)
        {
            try
            {
                var mcpLink = new MCPLink()
                {
                    SiteInformationId = request.SiteInformationId,
                    MineInformationId = request.MineInformationId,
                    MCPBoardId = request.MCPBoardId,
                    Panel = request.Panel,
                    FunctionalLocationId = request.FunctionalLocationId,
                    Link = request.Link,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false,
                    UpdatedAt = DateTime.UtcNow,
                };
                await _mcpLinkRepository.InsertAsync(mcpLink);
                return new MCPLinkResponse<bool>() { Code = 200, Message = "MCP Link Successfully Created" };
            }
            catch (Exception ex)
            {
                return new MCPLinkResponse<bool>() { Code = 400, Message = ex.Message };
            }
        }

        public async Task<MCPLinkResponse<bool>> UpdateAsync(MCPLinkDTO request)
        {
            try
            {
                var mcpLink = new MCPLink()
                {
                    SiteInformationId = request.SiteInformationId,
                    MineInformationId = request.MineInformationId,
                    MCPBoardId = request.MCPBoardId,
                    Panel = request.Panel,
                    FunctionalLocationId = request.FunctionalLocationId,
                    Link = request.Link,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false,
                    UpdatedAt = DateTime.UtcNow
                };
                await _mcpLinkRepository.UpdateAsync(mcpLink);
                return new MCPLinkResponse<bool>() { Code = 200, Message = "MCP Link Successfully Updated" };
            }
            catch (Exception ex)
            {
                return new MCPLinkResponse<bool>() { Code = 400, Message = ex.Message };
            }
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {

            try
            {
                var userDB = await _mcpLinkRepository.FindOneAsync(x => x.Id == id);
                await _mcpLinkRepository.DeleteAsync(userDB);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<MCPLinkResponse<MCPLinkList>> GetByIdAsync(string id)
        {
            try
            {
                var res = await _mcpLinkRepository.FindOneAsync(obj => obj.Id == id);
                return new MCPLinkResponse<MCPLinkList>()
                {
                    Code = 200,
                    Message = "MCP Link Retrive Successfully",
                    Data = new MCPLinkList()
                    {
                        Id = res.Id,
                        SiteInformationId = res.SiteInformationId,
                        MineInformationId = res.MineInformationId,
                        MCPBoardId = res.MCPBoardId,
                        Panel = res.Panel,
                        FunctionalLocationId = res.FunctionalLocationId,
                        Link = res.Link
                    }
                };
            }
            catch (Exception ex)
            {
                return new MCPLinkResponse<MCPLinkList>()
                {
                    Code = 400,
                    Message = ex.Message
                };
            }
        }

        public async Task<MCPLinkResponse<List<MCPLinkList>>> ListAsync()
        {
            try
            {
                var res = await _mcpLinkRepository.FindAllAsync(_ => true);
                var list = res.Select(x => new MCPLinkList()
                {
                    Id = x.Id,
                    SiteInformationId = x.SiteInformationId,
                    MineInformationId = x.MineInformationId,
                    MCPBoardId = x.MCPBoardId,
                    Panel = x.Panel,
                    FunctionalLocationId = x.FunctionalLocationId,
                    Link = x.Link
                }).ToList();
                return new MCPLinkResponse<List<MCPLinkList>>() { Code = 200, Message = "All MCP Link Retrive Successfully", Data = list };
            }
            catch (Exception ex)
            {
                return new MCPLinkResponse<List<MCPLinkList>>() { Code = 200, Message = ex.Message };
            }
        }

       
    }
}
