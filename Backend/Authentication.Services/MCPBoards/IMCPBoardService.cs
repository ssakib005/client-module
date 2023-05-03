using Authentication.Services.MCPBoards.Request;
using Authentication.Services.MCPBoards.Response;

namespace Authentication.Services.MCPBoards
{
    public interface IMCPBoardService
    {
        Task<MCPBoardResponse<bool>> CreateAsync(MCPBoardDTO request);
        Task<MCPBoardResponse<bool>> UpdateAsync(MCPBoardDTO request);
        Task<MCPBoardResponse<List<MCPBoardList>>> ListAsync();
        Task<MCPBoardResponse<MCPBoardList>> GetByIdAsync(string id);
        Task<bool> DeleteByIdAsync(string id);
    }
}
