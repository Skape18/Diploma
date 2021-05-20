using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthorizationService.DbContext;
using AuthorizationService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AuthorizationService.Services
{
    public class UserService : IUserService
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private ApplicationContext _context;

        public UserService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ApplicationContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<bool> IsUserInRoleAsync(string id, string role)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<string> SignInAsync(LoginModel loginDto, string tokenKey, int tokenLifetime, string tokenAudience, string tokenIssuer)
        {
            var result = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, false, false);
            if (!result.Succeeded) return string.Empty;

            var user = await _userManager.FindByNameAsync(loginDto.UserName);

            var token = await GenerateJwtToken(user, tokenKey, tokenLifetime, tokenAudience, tokenIssuer);

            return token;
        }

        public async Task<string> SignUpAsync(RegistrationModel registrationDto, string tokenKey, int tokenLifetime, string tokenAudience, string tokenIssuer)
        {
            var applicationUser = new IdentityUser
            {
                UserName = registrationDto.UserName,
                Email = registrationDto.Email,
            };
            var result = await _userManager.CreateAsync(applicationUser, registrationDto.Password);

            if (!result.Succeeded) return string.Empty;

            await _signInManager.SignInAsync(applicationUser, false);

            string token = await GenerateJwtToken(applicationUser, tokenKey, tokenLifetime, tokenAudience, tokenIssuer);

            return token;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        private async Task<string> GenerateJwtToken(IdentityUser user, string tokenKey, int tokenLifetime, string tokenAudience, string tokenIssuer)
        {
            var utcNow = DateTime.UtcNow;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims);

            var roles = await _userManager.GetRolesAsync(user);

            claimsIdentity.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                signingCredentials: signingCredentials,
                claims: claimsIdentity.Claims,
                notBefore: utcNow,
                expires: utcNow.AddMinutes(tokenLifetime),
                audience: tokenAudience,
                issuer: tokenIssuer
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
