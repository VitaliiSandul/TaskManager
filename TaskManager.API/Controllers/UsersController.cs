using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Domain.Models;
using TaskManager.API.Domain.Services;
using TaskManager.API.Extensions;
using TaskManager.API.Filters;
using TaskManager.API.Resources;

namespace TaskManager.API.Controllers
{
    [Route("/api/[controller]")]
    [Authorize(Roles="Admin")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IAppUserService userService;
        private readonly IMapper mapper;
        public UsersController(IAppUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<AppUserResource>> GetAllAsync()
        {
            var users = await userService.ListAsync();
            var resources = mapper.Map<IEnumerable<AppUser>, IEnumerable<AppUserResource>>(users);
            return resources;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]SaveAppUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            
            var user = mapper.Map<SaveAppUserResource, AppUser>(resource);
            var result = await userService.SaveAsync(user);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var userResource = mapper.Map<AppUser, AppUserResource>(result.AppUser);
            return Ok(userResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody]SaveAppUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var user = mapper.Map<SaveAppUserResource, AppUser>(resource);
            var result = await userService.UpdateAsync(id, user);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = mapper.Map<AppUser, AppUserResource>(result.AppUser);
            return Ok(userResource);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await userService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = mapper.Map<AppUser, AppUserResource>(result.AppUser);
            return Ok(userResource);
        }


        [HttpGet("search")]
        public async Task<IActionResult> SearchAsync([FromQuery] UserSearchOptions searchOptions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid search options");
            }

            var users = await userService.SearchAsync(searchOptions);
            var searchResult = mapper.Map<IEnumerable<AppUser>, IEnumerable<AppUserResource>>(users);

            return Ok(searchResult);
        }
    }
}