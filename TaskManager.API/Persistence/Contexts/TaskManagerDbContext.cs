using Microsoft.EntityFrameworkCore;
using TaskManager.API.Domain.Models;

namespace TaskManager.API.Persistence.Contexts
{
    public class TaskManagerDbContext:DbContext
    {
        
        public DbSet<AppTask> AppTask{ get; set; }
        public DbSet<AppUser> AppUser{ get; set; }
        public DbSet<AppRole> AppRole{ get; set; }        
        public DbSet<UserRole> UserRole {get;set;}
        public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options):base(options)
        {            
        }

    }
}