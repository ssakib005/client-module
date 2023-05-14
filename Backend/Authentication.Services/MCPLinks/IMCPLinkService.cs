using Authentication.Services.MCPLinks.Request;
using Authentication.Services.MCPLinks.Response;

namespace Authentication.Services.MCPLinks
{
    public interface IMCPLinkService
    {
        Task<MCPLinkResponse<bool>> CreateAsync(MCPLinkDTO request);
        Task<MCPLinkResponse<bool>> UpdateAsync(MCPLinkDTO request);
        Task<MCPLinkResponse<List<MCPLinkList>>> ListAsync();
        Task<MCPLinkResponse<MCPLinkList>> GetByIdAsync(string id);
        Task<bool> DeleteByIdAsync(string id);
        Task<MCPLinkResponse<List<MCPLinkList>>> GetListByMineAndSiteInformationId(string mid, string sid);
    }
}
