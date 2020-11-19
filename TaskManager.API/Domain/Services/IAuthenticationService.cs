using System.Threading.Tasks;
using TaskManager.API.Domain.Services.Communication;

namespace TaskManager.API.Domain.Services
{
    public interface IAuthenticationService
    {
         Task<AppUserResponse> AuthenticateAsync(string login, string password);
    }
}