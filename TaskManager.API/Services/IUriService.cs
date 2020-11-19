using System;
using TaskManager.API.Filters;

namespace TaskManager.API.Services
{
    public interface IUriService
    {
        Uri GetPageUri(PaginationFilter filter, string route);
    }
}