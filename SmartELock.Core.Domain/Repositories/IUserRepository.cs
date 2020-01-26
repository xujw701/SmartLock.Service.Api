using SmartELock.Core.Domain.Models;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<int> CreateUser(User user);
        Task<User> GetUser(int userId);
    }
}
