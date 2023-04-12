using Authentication.Core.Interfaces;
using Authentication.Core.Models;
using Authentication.Core.MongoDb.Repository;
using Authentication.Core.Options;
using Authentication.Services.FunctionalLocation.Request;
using Authentication.Services.FunctionalLocation.Response;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Services.FunctionalLocation
{
    public class FunctionalLocation : IFunctionalLocation
    {
        private readonly ApiConfigurationOptions _apiConfig;
        private readonly IMongoDbRepository<User> _userRepository;
        private readonly ISessionService _sessionService;
        public FunctionalLocation(IOptions<ApiConfigurationOptions> apiConfig, IMongoDbRepository<User> userRepository, ISessionService sessionService)
        {
            _apiConfig = apiConfig.Value;
            _userRepository = userRepository;
            _sessionService = sessionService;
        }


        public Task<FunctionalLocationResponse> CreateAsync(FunctionalLocationDTO request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<FunctionalLocationResponse> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> ListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<FunctionalLocationResponse> UpdateAsync(FunctionalLocationDTO request)
        {
            throw new NotImplementedException();
        }
    }
}
