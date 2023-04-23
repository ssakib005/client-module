using Authentication.Core.Interfaces;
using Authentication.Core.MongoDb.Repository;
using Authentication.Core.Options;
using Microsoft.Extensions.Options;
using Authentication.Core.Models;
using Authentication.Services.SiteInformations.Request;
using Authentication.Services.SiteInformations.Response;

namespace Authentication.Services.SiteInformations
{
    public class SiteInformationService : ISiteInformationService
    {
        private readonly ApiConfigurationOptions _apiConfig;
        private readonly IMongoDbRepository<SiteInformation> _siteInformationRepository;
        private readonly IMongoDbRepository<FunctionalLocation> _functionalLocationRepository;
        private readonly ISessionService _sessionService;
        public SiteInformationService(
            IOptions<ApiConfigurationOptions> apiConfig,
            IMongoDbRepository<SiteInformation> siteInformationRepository,
            IMongoDbRepository<FunctionalLocation> functionalLocationRepository,
            ISessionService sessionService)
        {
            _apiConfig = apiConfig.Value;
            _siteInformationRepository = siteInformationRepository;
            _functionalLocationRepository = functionalLocationRepository;
            _sessionService = sessionService;
        }


        public async Task<SiteInformationsResponse<bool>> CreateAsync(SiteInformationsDTO request)
        {
            try
            {
                var siteInformation = new SiteInformation()
                {
                    Name = request.Name,
                    Description = request.Description,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false,
                    UpdatedAt = DateTime.UtcNow,
                    FilePath = request.Image,
                    FunctionalLocationIds = request.FunctionalLocationIds
                };
                await _siteInformationRepository.InsertAsync(siteInformation);
                return new SiteInformationsResponse<bool>() { Code = 200, Message = "Site Information Successfully Created" };
            }
            catch (Exception ex)
            {
                return new SiteInformationsResponse<bool>() { Code = 400, Message = ex.Message };
            }
        }

        public async Task<SiteInformationsResponse<bool>> UpdateAsync(SiteInformationsDTO request)
        {
            try
            {
                var siteInformation = new SiteInformation()
                {
                    Id = request.Id,
                    Name = request.Name,
                    Description = request.Description,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false,
                    UpdatedAt = DateTime.UtcNow,
                    FilePath = request.Image,
                    FunctionalLocationIds = request.FunctionalLocationIds
                };
                await _siteInformationRepository.UpdateAsync(siteInformation);
                return new SiteInformationsResponse<bool>() { Code = 200, Message = "Site Information Successfully Updated" };
            }
            catch (Exception ex)
            {
                return new SiteInformationsResponse<bool>() { Code = 400, Message = ex.Message };
            }
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {

            try
            {
                var userDB = await _siteInformationRepository.FindOneAsync(x => x.Id == id);
                await _siteInformationRepository.DeleteAsync(userDB);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<SiteInformationsResponse<SiteInformationsList>> GetByIdAsync(string id)
        {
            try
            {
                List<string> list = new List<string>();

                var res = await _siteInformationRepository.FindOneAsync(obj => obj.Id == id);
                
                return new SiteInformationsResponse<SiteInformationsList>()
                {
                    Code = 200,
                    Message = "Site Information Retrive Successfully",
                    Data = new SiteInformationsList()
                    {
                        Id = res.Id,
                        Name = res.Name,
                        Description = res.Description,
                        Image = res.FilePath,
                        FunctionalLocationList = res.FunctionalLocationIds
                    }
                };
            }
            catch (Exception ex)
            {
                return new SiteInformationsResponse<SiteInformationsList>()
                {
                    Code = 400,
                    Message = ex.Message
                };
            }
        }

        public async Task<SiteInformationsResponse<List<SiteInformationsList>>> ListAsync()
        {
            try
            {
                var res = await _siteInformationRepository.FindAllAsync(_ => true);
                var list = res.Select(x => new SiteInformationsList()
                {
                    Description = x.Description,
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.FilePath
                }).ToList();
                return new SiteInformationsResponse<List<SiteInformationsList>>() { Code = 200, Message = "Site Information Successfully Created", Data = list };
            }
            catch (Exception ex)
            {
                return new SiteInformationsResponse<List<SiteInformationsList>>() { Code = 200, Message = ex.Message };
            }
        }

       
    }
}
