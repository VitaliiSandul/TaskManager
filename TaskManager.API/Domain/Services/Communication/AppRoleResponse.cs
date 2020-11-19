using TaskManager.API.Domain.Models;

namespace TaskManager.API.Domain.Services.Communication
{
    public class AppRoleResponse : BaseResponse
    {
        public AppRole AppRole {get;set;}

        public AppRoleResponse(bool success, string message, AppRole appRole):base(success, message)
        {
            AppRole = appRole;
        }

        public AppRoleResponse(AppRole appRole): this(true, string.Empty, appRole){}
        public AppRoleResponse(string message): this(false, message, null) {}
    }
}
