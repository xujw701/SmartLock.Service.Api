using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Commands;
using System;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Services
{
    public interface ISuperAdminService
    {
        Task<int> CreateSuperAdmin(SuperAdminCreateCommand command);
        Task<SuperAdmin> Login(SuperAdminLoginCommand command);
        Task<Tuple<bool, SuperAdmin>> CheckToken(int superAdminId, string token);
        Task<int> CreateKeyboxAsset(KeyboxAssetCreateCommand command);
    }
}
