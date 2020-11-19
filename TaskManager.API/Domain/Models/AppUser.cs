using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.API.Domain.Models
{
    public class AppUser
    {
        [Key]
        public int UserId {get;set;}
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public string Email {get;set;}
        public string Photo {get;set;}
        public DateTime Birthday {get;set;}
        public string Phone {get;set;}
        public string Login {get;set;}
        public string Password {get;set;}
        public string Token {get;set;}
        public IList<UserRole> UserRoles {get;set;} = new List<UserRole>();
    }
}