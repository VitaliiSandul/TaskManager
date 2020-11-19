using System.Threading.Tasks;

namespace TaskManager.API.Domain.Repositories
{
    public interface IUnitOfWork
    {
         Task CompleteAsync();
    }
}