using System;

namespace TaskManager.API.Resources
{
    public class AppTaskResource
    {
        public int TaskId {get;set;}
        public int UserId {get;set;}
        public string TaskTitle {get;set;}
        public string TaskDetails {get;set;}
        public DateTime TaskCreationDate {get;set;}
        public bool TaskStatus {get;set;}
        public string TaskPriority {get;set;}
    }
}