using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Commands;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Services
{
    public interface IUserService
    {
        Task<int> CreateUser(UserCreateCommand command);
        Task<User> Login(UserLoginCommand command);
        Task<bool> CheckToken(int userId, string token);
    }
}
