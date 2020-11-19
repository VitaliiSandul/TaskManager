using System.Linq;
using AutoMapper;
using TaskManager.API.Domain.Models;
using TaskManager.API.Resources;

namespace Shop.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<AppTask, AppTaskResource>();
            CreateMap<AppUser, AppUserResource>();
            CreateMap<AppUser, AppUserResource>()
                .ForMember(x => x.Role, y => y.MapFrom(s => s.UserRoles.Select(z => z.AppRole.RoleName)));
        }
    }
}