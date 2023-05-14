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
        private readonly IMongoDbRepository<MineInformation> _mineInformationRepository;
        private readonly IMongoDbRepository<SiteInformation> _siteInformationRepository;
        private readonly IMongoDbRepository<MCPBoard> _mcpBoardRepository;
        private readonly IMongoDbRepository<FunctionalLocation> _functionalLocationRepository;
        private readonly ISessionService _sessionService;
        public MCPLinkService(
            IOptions<ApiConfigurationOptions> apiConfig,
            IMongoDbRepository<MCPLink> mcpLinkRepository,
            IMongoDbRepository<MineInformation> mineInformationRepository,
            IMongoDbRepository<SiteInformation> siteInformationRepository,
            IMongoDbRepository<MCPBoard> mcpBoardRepository,
            IMongoDbRepository<FunctionalLocation> functionLocationRepository,
            ISessionService sessionService)
        {
            _apiConfig = apiConfig.Value;
            _mcpLinkRepository = mcpLinkRepository;
            _mcpBoardRepository = mcpBoardRepository;
            _mineInformationRepository = mineInformationRepository;
            _siteInformationRepository = siteInformationRepository;
            _functionalLocationRepository = functionLocationRepository;
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
                    Id = request.Id,
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
        public async Task<MCPLinkResponse<List<MCPLinkList>>> GetListByMineAndSiteInformationId(string mid, string sid)
        {
            try
            {
                var res = await _mcpLinkRepository.FindAllAsync(obj => obj.MineInformationId == mid && obj.SiteInformationId == sid);
                
                var list = res.Select(x => new MCPLinkList()
                {
                    Id = x.Id,
                    SiteInformationId = x.SiteInformationId,
                    SiteInformationName = _siteInformationRepository.GetById(x.SiteInformationId).Name,
                    MineInformationId = x.MineInformationId,
                    MineInformationName = _mineInformationRepository.GetById(x.MineInformationId).Name,
                    MCPBoardId = x.MCPBoardId,
                    MCPBoardName = _mcpBoardRepository.GetById(x.MCPBoardId).Name,
                    Panel = x.Panel,
                    FunctionalLocationId = x.FunctionalLocationId,
                    FunctionalLocationName = _functionalLocationRepository.GetById(x.FunctionalLocationId).Name,
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
