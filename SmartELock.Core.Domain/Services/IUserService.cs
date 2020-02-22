using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Commands;
using System;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Services
{
    public interface IUserService
    {
        Task<int> CreateUser(UserCreateCommand command);
        Task<bool> UpdateMe(UserMeUpdateCommand command);
        Task<User> Login(UserLoginCommand command);
        Task<Tuple<bool, User>> CheckToken(int userId, string token);
        Task<int> Auth(UserLoginCommand command);
    }
}
