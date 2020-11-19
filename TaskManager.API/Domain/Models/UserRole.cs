using System.ComponentModel.DataAnnotations;

namespace TaskManager.API.Domain.Models
{
    public class UserRole
    {
        [Key]
        public int UserRoleId {get;set;}
        public int UserId {get;set;}
        public int RoleId {get;set;}
        public AppRole AppRole {get;set;}
        public AppUser AppUser {get;set;}
    }
}