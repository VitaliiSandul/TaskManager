using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.API.Domain.Models
{
    public class AppTask
    {
        [Key]
        public int TaskId {get;set;}
        public int UserId {get;set;}
        public string TaskTitle {get;set;}
        public string TaskDetails {get;set;}
        public DateTime TaskCreationDate {get;set;}
        public bool TaskStatus {get;set;}
        public string TaskPriority {get;set;}        
        public AppUser AppUser {get;set;}        
    }
}