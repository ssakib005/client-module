using Authentication.Core.Interfaces;
using Authentication.Core.MongoDb.Repository;
using Authentication.Core.Options;
using Microsoft.Extensions.Options;
using Authentication.Core.Models;
using Authentication.Services.MCPBoards.Request;
using Authentication.Services.MCPBoards.Response;

namespace Authentication.Services.MCPBoards
{
    public class MCPBoardService : IMCPBoardService
    {
        private readonly ApiConfigurationOptions _apiConfig;
        private readonly IMongoDbRepository<MCPBoard> _mcpBoardRepository;
        private readonly ISessionService _sessionService;
        public MCPBoardService(
            IOptions<ApiConfigurationOptions> apiConfig,
            IMongoDbRepository<MCPBoard> mcpBoardRepository,
            ISessionService sessionService)
        {
            _apiConfig = apiConfig.Value;
            _mcpBoardRepository = mcpBoardRepository;
            _sessionService = sessionService;
        }


        public async Task<MCPBoardResponse<bool>> CreateAsync(MCPBoardDTO request)
        {
            try
            {
                var location = new MCPBoard()
                {
                    Name = request.Name,
                    Description = request.Description,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false,
                    UpdatedAt = DateTime.UtcNow,
                };
                await _mcpBoardRepository.InsertAsync(location);
                return new MCPBoardResponse<bool>() { Code = 200, Message = "MCP Board Successfully Created" };
            }
            catch (Exception ex)
            {
                return new MCPBoardResponse<bool>() { Code = 400, Message = ex.Message };
            }
        }

        public async Task<MCPBoardResponse<bool>> UpdateAsync(MCPBoardDTO request)
        {
            try
            {
                var location = new MCPBoard()
                {
                    Id = request.Id,
                    Name = request.Name,
                    Description = request.Description,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false,
                    UpdatedAt = DateTime.UtcNow
                };
                await _mcpBoardRepository.UpdateAsync(location);
                return new MCPBoardResponse<bool>() { Code = 200, Message = "MCP Board Successfully Updated" };
            }
            catch (Exception ex)
            {
                return new MCPBoardResponse<bool>() { Code = 400, Message = ex.Message };
            }
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {

            try
            {
                var userDB = await _mcpBoardRepository.FindOneAsync(x => x.Id == id);
                await _mcpBoardRepository.DeleteAsync(userDB);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<MCPBoardResponse<MCPBoardList>> GetByIdAsync(string id)
        {
            try
            {
                var res = await _mcpBoardRepository.FindOneAsync(obj => obj.Id == id);
                return new MCPBoardResponse<MCPBoardList>()
                {
                    Code = 200,
                    Message = "MCP Board Retrive Successfully",
                    Data = new MCPBoardList()
                    {
                        Id = res.Id,
                        Name = res.Name,
                        Description = res.Description
                    }
                };
            }
            catch (Exception ex)
            {
                return new MCPBoardResponse<MCPBoardList>()
                {
                    Code = 400,
                    Message = ex.Message
                };
            }
        }

        public async Task<MCPBoardResponse<List<MCPBoardList>>> ListAsync()
        {
            try
            {
                var res = await _mcpBoardRepository.FindAllAsync(_ => true);
                var list = res.Select(x => new MCPBoardList()
                {
                    Description = x.Description,
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
                return new MCPBoardResponse<List<MCPBoardList>>() { Code = 200, Message = "MCP Board Successfully Created", Data = list };
            }
            catch (Exception ex)
            {
                return new MCPBoardResponse<List<MCPBoardList>>() { Code = 200, Message = ex.Message };
            }
        }

       
    }
}
