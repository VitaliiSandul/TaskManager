using System.ComponentModel.DataAnnotations;
using System;

namespace TaskManager.API.Resources
{
    public class SaveAppTaskResource
    {
        public int UserId {get;set;}
        [Required]
        [MaxLength(100)]
        public string TaskTitle {get;set;}
        public string TaskDetails {get;set;}
        public DateTime TaskCreationDate {get;set;}
        public bool TaskStatus {get;set;}
        public string TaskPriority {get;set;}
    }
}