using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Snapshots;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Repositories.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace SmartELock.Core.Repositories.Repositories
{
    public class SuperAdminRepository : ISuperAdminRepository
    {
        private readonly IDbRetryHandler _dbRetryHandler;

        public SuperAdminRepository(IDbRetryHandler dbRetryHandler)
        {
            _dbRetryHandler = dbRetryHandler;
        }

        public async Task<int> CreateSuperAdmin(SuperAdmin superAdmin)
        {
            var id = await _dbRetryHandler.QueryAsync(async connection =>
            {
                using (var reader = await connection.QueryMultipleAsync("SuperAdmin_Create", new
                {
                    superAdmin.Username,
                    superAdmin.Password
                }))
                {
                    return reader.Read<int>().Single();
                }
            });

            return id;
        }

        public async Task<SuperAdmin> GetSuperAdmin(int superAdminId)
        {
            var superAdmin = await _dbRetryHandler.QueryAsync(async connection =>
            {
                using (var reader = await connection.QueryMultipleAsync("SuperAdmin_Get", new
                {
                    superAdminId
                }))
                {
                    var snapshots = reader.Read<SuperAdminSnapshot>().ToList();

                    return snapshots.Select(snapshot => SuperAdmin.CreateFrom(snapshot)).FirstOrDefault();
                }
            });

            return superAdmin;
        }

        public async Task<SuperAdmin> GetSuperAdmin(string username)
        {
            var superAdmin = await _dbRetryHandler.QueryAsync(async connection =>
            {
                using (var reader = await connection.QueryMultipleAsync("SuperAdmin_GetByUsername", new
                {
                    username
                }))
                {
                    var snapshots = reader.Read<SuperAdminSnapshot>().ToList();

                    return snapshots.Select(snapshot => SuperAdmin.CreateFrom(snapshot)).FirstOrDefault();
                }
            });

            return superAdmin;
        }

        /// <summary>
        /// Internal function to update super admin's tokens
        /// </summary>
        /// <param name="userId">The id of the super admin</param>
        /// <param name="newToken">The new token (null to revoke)</param>
        /// <returns></returns>
        public async Task<bool> UpdateToken(int superAdminId, string newToken)
        {
            var result = await _dbRetryHandler.QueryAsync(async connection =>
            {
                return await connection.ExecuteAsync("SuperAdmin_Token", new
                {
                    superAdminId,
                    Token = newToken
                });
            });

            return result > 0;
        }
    }
}
