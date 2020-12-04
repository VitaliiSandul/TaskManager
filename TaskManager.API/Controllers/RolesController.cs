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
    public class RolesController : Controller
    {        
        private readonly IAppRoleService roleService;
        private readonly IMapper mapper;

        public RolesController(IAppRoleService roleService, IMapper mapper)
        {
            this.roleService = roleService;
            this.mapper = mapper;            
        }

        [HttpGet]
        public async Task<IEnumerable<AppRoleResource>> GetAllAsync()
        {
            var roles = await roleService.ListAsync();
            var resources = mapper.Map<IEnumerable<AppRole>, IEnumerable<AppRoleResource>>(roles);
            return resources;
        }
        
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]SaveAppRoleResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            
            var role = mapper.Map<SaveAppRoleResource, AppRole>(resource);
            var result = await roleService.SaveAsync(role);

            if (!result.Success)
                return BadRequest(result.Message);            
            
            var roleResource = mapper.Map<AppRole, AppRoleResource>(result.AppRole);

            return Ok(roleResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody]SaveAppRoleResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var role = mapper.Map<SaveAppRoleResource, AppRole>(resource);
            var result = await roleService.UpdateAsync(id, role);

            if (!result.Success)
                return BadRequest(result.Message);

            var roleResource = mapper.Map<AppRole, AppRoleResource>(result.AppRole);
            return Ok(roleResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await roleService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var roleResource = mapper.Map<AppRole, AppRoleResource>(result.AppRole);
            return Ok(roleResource);
        }
    }
}