using TaskManager.API.Domain.Models;

namespace TaskManager.API.Domain.Services.Communication
{
    public class AppUserResponse : BaseResponse
    {
        public AppUser AppUser {get;set;}

        public AppUserResponse(bool success, string message, AppUser appUser):base(success, message)
        {
            AppUser = appUser;
        }

        public AppUserResponse(AppUser appUser): this(true, string.Empty, appUser){}
        public AppUserResponse(string message): this(false, message, null) {}
    }
}