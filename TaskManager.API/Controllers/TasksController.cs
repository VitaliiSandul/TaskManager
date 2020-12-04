using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using TaskManager.API.Domain.Models;
using TaskManager.API.Domain.Services;
using TaskManager.API.Extensions;
using TaskManager.API.Resources;
using TaskManager.API.Filters;
using TaskManager.API.Wrappers;
using TaskManager.API.Services;
using TaskManager.API.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles="Admin,User")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        // GET api/values
        private readonly IAppTaskService appTaskService;
        private readonly IUriService uriService;
        private readonly IMapper mapper;

        public TasksController(IAppTaskService appTaskService, IUriService uriService, IMapper mapper)
        {
            this.appTaskService = appTaskService;
            this.uriService = uriService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<AppTaskResource>> GetAllAsync()
        {
            var appTasks = await appTaskService.ListAsync();
            var resources = mapper.Map<IEnumerable<AppTask>, IEnumerable<AppTaskResource>>(appTasks);
            return resources;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]SaveAppTaskResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            
            var appTask = mapper.Map<SaveAppTaskResource, AppTask>(resource);
            var result = await appTaskService.SaveAsync(appTask);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var appTaskResource = mapper.Map<AppTask, AppTaskResource>(result.AppTask);
            return Ok(appTaskResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody]SaveAppTaskResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var appTask = mapper.Map<SaveAppTaskResource, AppTask>(resource);
            var result = await appTaskService.UpdateAsync(id, appTask);

            if (!result.Success)
                return BadRequest(result.Message);

            var appTaskResource = mapper.Map<AppTask, AppTaskResource>(result.AppTask);
            return Ok(appTaskResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await appTaskService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var appTaskResource = mapper.Map<AppTask, AppTaskResource>(result.AppTask);
            return Ok(appTaskResource);
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> Pagination([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var appTasks = await appTaskService.ListAsync();
            var resources = mapper.Map<IEnumerable<AppTask>, IEnumerable<AppTaskResource>>(appTasks);

            var pagedData = resources
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();
            var totalRecords = resources.Count();
            var pagedReponse = PaginationHelper.CreatePagedResponse<AppTaskResource>(pagedData, validFilter, totalRecords, uriService, route);
            return Ok(pagedReponse);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchAsync([FromQuery] TaskSearchOptions searchOptions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid search options");
            }

            var appTasks = await appTaskService.SearchAsync(searchOptions);
            var searchResult = mapper.Map<IEnumerable<AppTask>, IEnumerable<AppTaskResource>>(appTasks);

            return Ok(searchResult);
        }

    }
}
