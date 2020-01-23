using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Commands;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Services
{
    public interface ISuperAdminService
    {
        Task<int> CreateSuperAdmin(SuperAdminCreateCommand command);
        Task<SuperAdmin> Login(SuperAdminLoginCommand command);
        Task<bool> CheckToken(int superAdminId, string token);
    }
}
