using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.API.Domain.Models;
using TaskManager.API.Domain.Repositories;
using TaskManager.API.Extensions;
using TaskManager.API.Filters;
using TaskManager.API.Persistence.Contexts;

namespace TaskManager.API.Persistence.Repositories
{
    public class AppUserRepository: BaseRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(TaskManagerDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<AppUser>> ListAsync()
        {
            return await context.AppUser.Include(x => x.UserRoles)
                                        .ThenInclude(x => x.AppRole)
                                            .ToListAsync();
        }
        public override async Task<AppUser> FindByIdAsync(int id)
        {
            return await context.AppUser 
                                    .Include(x => x.UserRoles)
                                        .ThenInclude(x => x.AppRole)
                                            .FirstOrDefaultAsync(x => x.UserId == id);
        }
        public async Task<IEnumerable<AppUser>> SearchAsync(UserSearchOptions searchOptions)
        {
            var predicate = PredicateBuilder.True<AppUser>();

            if(searchOptions.UserId > 0)
                predicate = predicate.And(x => x.UserId == searchOptions.UserId);

            if (!string.IsNullOrEmpty(searchOptions.FirstName))
                predicate = predicate.And(x => x.FirstName.Contains(searchOptions.FirstName));
                
            if (!string.IsNullOrEmpty(searchOptions.LastName))
                predicate = predicate.And(x => x.LastName.Contains(searchOptions.LastName));

            if (!string.IsNullOrEmpty(searchOptions.Email))
                predicate = predicate.And(x => x.Email.Contains(searchOptions.Email));
            
            if(searchOptions.Birthday != DateTime.MinValue) 
                predicate = predicate.And(x => x.Birthday == searchOptions.Birthday);

            if (!string.IsNullOrEmpty(searchOptions.Phone))
                predicate = predicate.And(x => x.Phone.Contains(searchOptions.Phone));

            if (!string.IsNullOrEmpty(searchOptions.Login))
                predicate = predicate.And(x => x.Login == searchOptions.Login);

            if (!string.IsNullOrEmpty(searchOptions.RoleName))
                predicate = predicate.And(x => x.UserRoles.Any(y =>y.AppRole.RoleName == searchOptions.RoleName));

            var pred = predicate.Compile();
            var search = await context.AppUser.Include(x => x.UserRoles)
                                                .ThenInclude(x => x.AppRole)
                                                    .ToListAsync();
            var result = search.Where(pred);
            return result;
        }
    }
}