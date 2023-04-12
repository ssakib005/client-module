using Authentication.Core.Models;
using Authentication.Services.Users.Request;
using Authentication.Services.Users.Response;
using DnsClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Services.Users
{
    public interface IUserService
    {

        Task<User> GetUserAsync(UserRequest request);

        Task<User> CreateUserAsync(RegistrationDTO request);
        Task<User> UpdateUserAsync(RegistrationDTO request);
        Task<List<User>> UserListAsync();

        Task<User> UserByIdAsync(string id);
        Task<bool> UserDeleteByIdAsync(string id);


        Task<CommonResponse> SaveProfileDetails(User user);
        Task<User> GetCurrentUserProfileDetails();
    }
}
