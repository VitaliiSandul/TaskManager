using TaskManager.API.Domain.Models;
using TaskManager.API.Domain.Services.Communication;

namespace TaskManager.API.Domain.Services.Communication
{
    public class SaveAppTaskResponse : BaseResponse
    {
        public AppTask AppTask {get;set;}
        public SaveAppTaskResponse(bool success, string message, AppTask appTask):base(success, message)
        {
            AppTask = appTask;
        }

        public SaveAppTaskResponse(AppTask appTask): this(true, string.Empty, appTask){}
        public SaveAppTaskResponse(string message): this(false, message, null) {}
    }
}