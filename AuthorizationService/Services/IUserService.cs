using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizationService.Models;

namespace AuthorizationService.Services
{
    public interface IUserService
    {
        Task<bool> IsUserInRoleAsync(string id, string role);

        Task<string> SignInAsync(LoginModel loginDto, string tokenKey, int tokenLifetime,
            string tokenAudience, string tokenIssuer);

        Task<string> SignUpAsync(RegistrationModel registrationDto, string tokenKey, int tokenLifetime,
            string tokenAudience, string tokenIssuer);

        Task SignOutAsync();
    }
}
