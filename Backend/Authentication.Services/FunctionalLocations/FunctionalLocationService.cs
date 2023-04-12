

using Authentication.Core.Interfaces;
using Authentication.Core.MongoDb.Repository;
using Authentication.Core.Options;
using Microsoft.Extensions.Options;
using Authentication.Core.Models;
using Authentication.Services.FunctionalLocations.Request;
using Authentication.Services.FunctionalLocations.Response;

namespace Authentication.Services.FunctionalLocations
{
    public class FunctionalLocationService : IFunctionalLocationService
    {
        private readonly ApiConfigurationOptions _apiConfig;
        private readonly IMongoDbRepository<FunctionalLocation> _locationRepository;
        private readonly ISessionService _sessionService;
        public FunctionalLocationService(
            IOptions<ApiConfigurationOptions> apiConfig,
            IMongoDbRepository<FunctionalLocation> locationRepository,
            ISessionService sessionService)
        {
            _apiConfig = apiConfig.Value;
            _locationRepository = locationRepository;
            _sessionService = sessionService;
        }


        public async Task<FunctionalLocationResponse<bool>> CreateAsync(FunctionalLocationDTO request)
        {
            try
            {
                var location = new FunctionalLocation()
                {
                    Name = request.Name,
                    Description = request.Description,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false,
                    UpdatedAt = DateTime.UtcNow,
                    FilePath = request.Image
                };
                await _locationRepository.InsertAsync(location);
                return new FunctionalLocationResponse<bool>() { Code = 200, Message = "Functional Location Successfully Created" };
            }
            catch (Exception ex)
            {
                return new FunctionalLocationResponse<bool>() { Code = 400, Message = ex.Message };
            }
        }

        public async Task<FunctionalLocationResponse<bool>> UpdateAsync(FunctionalLocationDTO request)
        {
            try
            {
                var location = new FunctionalLocation()
                {
                    Id = request.Id,
                    Name = request.Name,
                    Description = request.Description,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false,
                    UpdatedAt = DateTime.UtcNow,
                    FilePath = request.Image
                };
                await _locationRepository.UpdateAsync(location);
                return new FunctionalLocationResponse<bool>() { Code = 200, Message = "Functional Location Successfully Updated" };
            }
            catch (Exception ex)
            {
                return new FunctionalLocationResponse<bool>() { Code = 400, Message = ex.Message };
            }
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {

            try
            {
                var userDB = await _locationRepository.FindOneAsync(x => x.Id == id);
                await _locationRepository.DeleteAsync(userDB);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<FunctionalLocationResponse<FunctionalLocationList>> GetByIdAsync(string id)
        {
            try
            {
                var res = await _locationRepository.FindOneAsync(obj => obj.Id == id);
                return new FunctionalLocationResponse<FunctionalLocationList>()
                {
                    Code = 200,
                    Message = "Functional Location Retrive Successfully",
                    Data = new FunctionalLocationList()
                    {
                        Id = res.Id,
                        Name = res.Name,
                        Description = res.Description,
                        Image = res.FilePath
                    }
                };
            }
            catch (Exception ex)
            {
                return new FunctionalLocationResponse<FunctionalLocationList>()
                {
                    Code = 400,
                    Message = ex.Message
                };
            }
        }

        public async Task<FunctionalLocationResponse<List<FunctionalLocationList>>> ListAsync()
        {
            try
            {
                var res = await _locationRepository.FindAllAsync(_ => true);
                var list = res.Select(x => new FunctionalLocationList()
                {
                    Description = x.Description,
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.FilePath
                }).ToList();
                return new FunctionalLocationResponse<List<FunctionalLocationList>>() { Code = 200, Message = "Functional Location Successfully Created", Data = list };
            }
            catch (Exception ex)
            {
                return new FunctionalLocationResponse<List<FunctionalLocationList>>() { Code = 200, Message = ex.Message };
            }
        }

       
    }
}
