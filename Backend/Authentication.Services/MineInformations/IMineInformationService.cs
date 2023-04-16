using Authentication.Services.MineInformations.Request;
using Authentication.Services.MineInformations.Response;

namespace Authentication.Services.MineInformations
{
    public interface IMineInformationService
    {
        Task<MineInformationsResponse<bool>> CreateAsync(MineInformationsDTO request);
        Task<MineInformationsResponse<bool>> UpdateAsync(MineInformationsDTO request);
        Task<MineInformationsResponse<List<MineInformationsList>>> ListAsync();
        Task<MineInformationsResponse<MineInformationsList>> GetByIdAsync(string id);
        Task<bool> DeleteByIdAsync(string id);
    }
}
