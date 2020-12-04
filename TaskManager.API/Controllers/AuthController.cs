using System.Threading.Tasks;
using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Domain.Models;
using TaskManager.API.Domain.Services;
using TaskManager.API.Resources;

namespace TaskManager.API.Controllers
{
    [Route("/api/[controller]")]
    public class AuthController : Controller
    {        
        private readonly IAuthenticationService userService;
        private readonly IMapper mapper;

        public AuthController(IAuthenticationService userService,
                              IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] AppUser user)
        {
            var authenticatedUser = await userService.AuthenticateAsync(user.Login, user.Password);

            if (!authenticatedUser.Success)
                return BadRequest(authenticatedUser.Message);

            var userResource = mapper.Map<AppUser, AppUserResource>(authenticatedUser.AppUser);
            
            return Ok(userResource);
        }
    }
}
