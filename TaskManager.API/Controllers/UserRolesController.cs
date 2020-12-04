using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Domain.Models;
using TaskManager.API.Domain.Services;
using TaskManager.API.Extensions;
using TaskManager.API.Resources;

namespace TaskManager.API.Controllers
{
    [Route("/api/[controller]")]
    [Authorize(Roles="Admin")]
    [ApiController]
    public class UserRolesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUserRoleService userRoleService;
        public UserRolesController(IUserRoleService userRoleService, IMapper mapper)
        {
            this.mapper = mapper;
            this.userRoleService = userRoleService;
        }

        [HttpPost]
        [Route("setrole")]
        public async Task<IActionResult> SetRole([FromBody]SaveUserRoleResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var userResponse = await userRoleService.SetUserRoleAsync(resource.UserId, resource.RoleId);
            var appUserResource = mapper.Map<AppUser, AppUserResource>(userResponse.AppUser);
            
            return Ok(appUserResource);
        }

        [HttpDelete]
        [Route("deleterole/{id}")]
        public async Task<IActionResult> DeleteRole(int id, [FromBody]SaveUserRoleResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var userResponse = await userRoleService.DeleteRoleAsync(id, resource.RoleId);
            var appUserResource = mapper.Map<AppUser, AppUserResource>(userResponse.AppUser);
            
            return Ok(appUserResource);            
        }
    }
}