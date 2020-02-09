using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Commands;
using System;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Services
{
    public interface IAuthorizationService
    {
        Task<Tuple<bool, SuperAdmin>> CheckAdminToken(int superAdminId, string token);
        Task<Tuple<bool, User>> CheckUserToken(int userId, string token);
    }
}
