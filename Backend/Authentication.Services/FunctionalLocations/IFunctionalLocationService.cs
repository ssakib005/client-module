using Authentication.Services.FunctionalLocations.Request;
using Authentication.Services.FunctionalLocations.Response;

namespace Authentication.Services.FunctionalLocations
{
    public interface IFunctionalLocationService
    {
        Task<FunctionalLocationResponse<bool>> CreateAsync(FunctionalLocationDTO request);
        Task<FunctionalLocationResponse<bool>> UpdateAsync(FunctionalLocationDTO request);
        Task<FunctionalLocationResponse<List<FunctionalLocationList>>> ListAsync();

        Task<FunctionalLocationResponse<FunctionalLocationList>> GetByIdAsync(string id);
        Task<bool> DeleteByIdAsync(string id);
    }
}
