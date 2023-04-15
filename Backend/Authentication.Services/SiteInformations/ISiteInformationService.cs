using Authentication.Services.SiteInformations.Request;
using Authentication.Services.SiteInformations.Response;

namespace Authentication.Services.SiteInformations
{
    public interface ISiteInformationService
    {
        Task<SiteInformationsResponse<bool>> CreateAsync(SiteInformationsDTO request);
        Task<SiteInformationsResponse<bool>> UpdateAsync(SiteInformationsDTO request);
        Task<SiteInformationsResponse<List<SiteInformationsList>>> ListAsync();

        Task<SiteInformationsResponse<SiteInformationsList>> GetByIdAsync(string id);
        Task<bool> DeleteByIdAsync(string id);
    }
}
