using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.API.Domain.Models
{
    public class AppRole
    {
        [Key]
        public int RoleId {get;set;}
        public string RoleName {get;set;}        
        public IList<UserRole> UserRoles {get;set;} = new List<UserRole>();
    }
}