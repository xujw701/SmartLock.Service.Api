using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Commands;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Services
{
    public interface IAuthorizationService
    {
        Task<bool> CheckAdminToken(int superAdminId, string token);
        Task<bool> CheckUserToken(int userId, string token);
    }
}
