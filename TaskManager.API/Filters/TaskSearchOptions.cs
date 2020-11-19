using System;

namespace TaskManager.API.Filters
{
    public class TaskSearchOptions
    {
        public int UserId {get;set;}
        public string TaskTitle {get;set;}
        public string TaskDetails {get;set;}
        public DateTime TaskCreationDate {get;set;}
        public DateTime StartDate {get;set;}
        public DateTime EndDate {get;set;}
        public bool TaskStatus {get;set;}
        public string TaskPriority {get;set;} 
    }
}