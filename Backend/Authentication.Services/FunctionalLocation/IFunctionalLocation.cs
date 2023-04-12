using Authentication.Core.Models;
using Authentication.Services.FunctionalLocation.Request;
using Authentication.Services.FunctionalLocation.Response;
using Authentication.Services.Users.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Services.FunctionalLocation
{
    public interface IFunctionalLocation
    {
        Task<FunctionalLocationResponse> CreateAsync(FunctionalLocationDTO request);
        Task<FunctionalLocationResponse> UpdateAsync(FunctionalLocationDTO request);
        Task<List<User>> ListAsync();

        Task<FunctionalLocationResponse> GetByIdAsync(string id);
        Task<bool> DeleteByIdAsync(string id);
    }
}
