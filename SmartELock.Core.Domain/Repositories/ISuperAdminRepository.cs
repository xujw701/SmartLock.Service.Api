﻿using SmartELock.Core.Domain.Models;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Repositories
{
    public interface ISuperAdminRepository
    {
        Task<int> CreateSuperAdmin(SuperAdmin superAdmin);
        Task<SuperAdmin> GetSuperAdmin(int superAdminId);
        Task<SuperAdmin> GetSuperAdmin(string username);
        Task<bool> UpdateToken(int superAdminId, string newToken);
    }
}
