using SmartELock.Core.Domain.Models;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<int> CreateUser(User user);
        Task<bool> UpdateUser(User user);
        Task<User> GetUser(int userId);
        Task<User> GetUser(string username);
        Task<bool> UpdateToken(int userId, string newToken);
    }
}
