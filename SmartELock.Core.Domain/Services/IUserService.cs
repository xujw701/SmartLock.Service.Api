using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Services
{
    public interface IUserService
    {
        Task<int> CreateUser(UserCreateCommand command);
        Task<bool> UpdateMe(UserMeUpdateCommand command);
        Task<bool> UpdateUser(User currentUser, UserUpdateCommand command);
        Task<User> Login(UserLoginCommand command);
        Task<Tuple<bool, User>> CheckToken(int userId, string token);
        Task<int> Auth(UserLoginCommand command);
        Task<List<User>> GetUsers(User currentUser, int branchId);
        Task<int> UpdatePortrait(int userId, byte[] bytes, FileType fileType);
        Task<byte[]> GetPortrait(int portraitId);
    }
}
