using Authentication.Core.Interfaces;
using Authentication.Core.MongoDb.Repository;
using Authentication.Core.Options;
using Microsoft.Extensions.Options;
using Authentication.Core.Models;
using Authentication.Services.MineInformations.Request;
using Authentication.Services.MineInformations.Response;

namespace Authentication.Services.MineInformations
{
    public class MineInformationService : IMineInformationService
    {
        private readonly ApiConfigurationOptions _apiConfig;
        private readonly IMongoDbRepository<SiteInformation> _siteInformationRepository;
        private readonly IMongoDbRepository<MineInformation> _mineInformationRepository;
        private readonly ISessionService _sessionService;
        public MineInformationService(
            IOptions<ApiConfigurationOptions> apiConfig,
            IMongoDbRepository<SiteInformation> siteInformationRepository,
            IMongoDbRepository<MineInformation> mineInformationRepository,
            ISessionService sessionService)
        {
            _apiConfig = apiConfig.Value;
            _siteInformationRepository = siteInformationRepository;
            _mineInformationRepository = mineInformationRepository;
            _sessionService = sessionService;
        }


        public async Task<MineInformationsResponse<bool>> CreateAsync(MineInformationsDTO request)
        {
            try
            {
                var mineInformation = new MineInformation()
                {
                    Name = request.Name,
                    Description = request.Description,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false,
                    UpdatedAt = DateTime.UtcNow,
                    //FilePath = request.Image,
                    SiteInformationIds = request.SiteInformationIds
                };
                await _mineInformationRepository.InsertAsync(mineInformation);
                return new MineInformationsResponse<bool>() { Code = 200, Message = "Mine Information Successfully Created" };
            }
            catch (Exception ex)
            {
                return new MineInformationsResponse<bool>() { Code = 400, Message = ex.Message };
            }
        }

        public async Task<MineInformationsResponse<bool>> UpdateAsync(MineInformationsDTO request)
        {
            try
            {
                var mineInformation = new MineInformation()
                {
                    Id = request.Id,
                    Name = request.Name,
                    Description = request.Description,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false,
                    UpdatedAt = DateTime.UtcNow,
                    //FilePath = request.Image,
                    SiteInformationIds = request.SiteInformationIds
                };
                await _mineInformationRepository.UpdateAsync(mineInformation);
                return new MineInformationsResponse<bool>() { Code = 200, Message = "Mine Information Successfully Updated" };
            }
            catch (Exception ex)
            {
                return new MineInformationsResponse<bool>() { Code = 400, Message = ex.Message };
            }
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {

            try
            {
                var userDB = await _mineInformationRepository.FindOneAsync(x => x.Id == id);
                await _mineInformationRepository.DeleteAsync(userDB);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<MineInformationsResponse<MineInformationsList>> GetByIdAsync(string id)
        {
            try
            {   
                var res = await _mineInformationRepository.FindOneAsync(obj => obj.Id == id);
                return new MineInformationsResponse<MineInformationsList>()
                {
                    Code = 200,
                    Message = "Mine Information Retrive Successfully",
                    Data = new MineInformationsList()
                    {
                        Id = res.Id,
                        Name = res.Name,
                        Description = res.Description,
                        //Image = res.FilePath,
                        SiteInformationList = res.SiteInformationIds
                    }
                };
            }
            catch (Exception ex)
            {
                return new MineInformationsResponse<MineInformationsList>()
                {
                    Code = 400,
                    Message = ex.Message
                };
            }
        }

        public async Task<MineInformationsResponse<List<MineInformationsList>>> ListAsync()
        {
            try
            {
                var res = await _mineInformationRepository.FindAllAsync(_ => true);
                var list = res.Select(x => new MineInformationsList()
                {
                    Description = x.Description,
                    Id = x.Id,
                    Name = x.Name,
                    //Image = x.FilePath
                }).ToList();
                return new MineInformationsResponse<List<MineInformationsList>>() { Code = 200, Message = "Mine Information Successfully Created", Data = list };
            }
            catch (Exception ex)
            {
                return new MineInformationsResponse<List<MineInformationsList>>() { Code = 200, Message = ex.Message };
            }
        }

       
    }
}
