using System;

namespace TaskManager.API.Resources
{
    public class AppUserResource
    {
        public int UserId {get;set;}
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public string Email {get;set;}
        public string Photo {get;set;}
        public DateTime Birthday {get;set;}
        public string Phone {get;set;}
        public string Login {get;set;}
        public string Token {get;set;}
        public string[] Role {get;set;}
    }
}