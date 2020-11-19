using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TaskManager.API.Domain.Models;
using TaskManager.API.Domain.Services;
using TaskManager.API.Domain.Services.Communication;
using TaskManager.API.Domain.Repositories;
using TaskManager.API.Helpers;

namespace TaskManager.API.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AppSettings appSettings;
        private readonly IAppUserRepository userRepository;

        AppUser appUser;
        public AuthenticationService(IOptions<AppSettings> appSettings, IAppUserRepository userRepository)
        {
            this.appSettings = appSettings.Value;
            this.userRepository = userRepository;            
        }
        
        public async Task<AppUserResponse> AuthenticateAsync(string login, string password)
        {
            var users = await userRepository.ListAsync();
            appUser = users.FirstOrDefault(x => x.Login == login);
            if (appUser == null)
                return new AppUserResponse("Error: wrong login");

            if (login == appUser.Login && password == appUser.Password)
            {
                
                var roles = appUser.UserRoles.Select(x => x.AppRole.RoleName);
                
                var userRole = roles.Contains("Admin") ? "Admin" : appUser.UserRoles[0].AppRole.RoleName;

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, appUser.FirstName),
                        new Claim(ClaimTypes.Role, userRole)
                    }),
                    Expires = DateTime.Now.AddMinutes(appSettings.TokenExpires),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                appUser.Token = tokenHandler.WriteToken(token);
                appUser.Password = null;

                return new AppUserResponse(appUser);
            }
            else 
                return new AppUserResponse("Error: wrong password");
        }
    }
}