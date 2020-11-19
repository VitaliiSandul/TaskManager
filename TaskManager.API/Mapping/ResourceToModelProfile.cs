using AutoMapper;
using TaskManager.API.Domain.Models;
using TaskManager.API.Resources;

namespace TaskManager.API.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveAppTaskResource, AppTask>();   
            CreateMap<AppUserResource, AppUser>(); 
            CreateMap<SaveAppUserResource, AppUser>();                               
        }
    }
}