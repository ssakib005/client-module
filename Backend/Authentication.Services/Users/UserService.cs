using Authentication.Core.Interfaces;
using Authentication.Core.Models;
using Authentication.Core.MongoDb.Repository;
using Authentication.Core.Options;
using Authentication.Services.Users.Request;
using Authentication.Services.Users.Response;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Services.Users
{
    public class UserService : IUserService
    {
        private readonly ApiConfigurationOptions _apiConfig;
        private readonly IMongoDbRepository<User> _userRepository;
        private readonly ISessionService _sessionService;
        public UserService(IOptions<ApiConfigurationOptions> apiConfig, IMongoDbRepository<User> userRepository, ISessionService sessionService)
        {
            _apiConfig = apiConfig.Value;
            _userRepository = userRepository;
            _sessionService = sessionService;
        }

        public async Task<User> GetUserAsync(UserRequest request)
        {
            var userDB = await _userRepository.FindOneAsync(x => (x.Email.ToLower().Contains(request.Email.ToLower())) &&
                       request.Password.Equals(x.Password));

            return userDB;
        }

        public async Task<User> CreateUserAsync(RegistrationDTO request)
        {
            var userDB = await _userRepository.FindOneAsync(x => request.Email.Length == x.Email.Length && x.Email.ToLower().Contains(request.Email.ToLower()));

            if (userDB != null)
                return new User { Username = "User already exists with this email,try another one" };

            return await _userRepository.InsertAsync(new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Username = request.Username,
                Password = request.Password
            });

        }

        public async Task<CommonResponse> SaveProfileDetails(User user)
        {
            var dbUser = await _userRepository.GetByIdAsync(user.Id);
            if (dbUser != null)
            {
                dbUser.FirstName = user.FirstName;
                dbUser.LastName = user.LastName;
                dbUser.Email = user.Email;
                dbUser.LogoBits = user.LogoBits;

                await _userRepository.UpdateAsync(dbUser);

                return new CommonResponse { Success = true, Message = "Profile details saves successfully" };
            }

            return new CommonResponse
            {
                Message = "Profile not found to update"
            };
        }

        public async Task<User> GetCurrentUserProfileDetails()
        {
            var dbUser = await _userRepository.GetByIdAsync(_sessionService.GetUserId());

            if (dbUser != null)
            {
                dbUser.Username = String.Empty;
            }

            return dbUser;
        }

        public async Task<List<User>> UserListAsync()
        {
            return await _userRepository.FindAllAsync(_ => true);
        }

        public async Task<User> UserByIdAsync(string id)
        {
            return await _userRepository.FindOneAsync(obj => obj.Id == id);
        }

        public async Task<User> UpdateUserAsync(RegistrationDTO request)
        {
            var userDB = await _userRepository.FindOneAsync(x => x.Id == request.Id);

            if (userDB == null)
                return new User { Username = "User not exist" };

            return await _userRepository.UpdateAsync(new User
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Username = request.Username,
                Password = request.Password
            });

        }

        public async Task<bool> UserDeleteByIdAsync(string id)
        {
            try
            {
                var userDB = await _userRepository.FindOneAsync(x => x.Id == id);
                await _userRepository.DeleteAsync(userDB);
                return true;
            }catch (Exception ex)
            {
                return false;
            }
            
        }
    }
}
