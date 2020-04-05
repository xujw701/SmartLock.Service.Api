using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Snapshots;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Repositories.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartELock.Core.Repositories.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbRetryHandler _dbRetryHandler;

        public UserRepository(IDbRetryHandler dbRetryHandler)
        {
            _dbRetryHandler = dbRetryHandler;
        }

        public async Task<int> CreateUser(User user)
        {
            var id = await _dbRetryHandler.QueryAsync(async connection =>
            {
                using (var reader = await connection.QueryMultipleAsync("User_Create", new
                {
                    user.CompanyId,
                    user.BranchId,
                    user.FirstName,
                    user.LastName,
                    user.Email,
                    user.Phone,
                    user.Username,
                    user.Password,
                    user.Individual,
                    user.UserRoleId
                }))
                {
                    return reader.Read<int>().Single();
                }
            });

            return id;
        }

        public async Task<bool> UpdateUser(User user)
        {
            var result = await _dbRetryHandler.QueryAsync(async connection =>
            {
                return await connection.ExecuteAsync("User_Update", new
                {
                    user.UserId,
                    user.CompanyId,
                    user.BranchId,
                    user.FirstName,
                    user.LastName,
                    user.Email,
                    user.Phone,
                    user.Username,
                    user.Password,
                    user.Individual,
                    user.UserRoleId,
                    user.ResPortraitId
                });
            });

            return result > 0;
        }

        public async Task<User> GetUser(int userId)
        {
            var user = await _dbRetryHandler.QueryAsync(async connection =>
            {
                using (var reader = await connection.QueryMultipleAsync("User_Get", new
                {
                    userId
                }))
                {
                    var snapshots = reader.Read<UserSnapshot>().ToList();

                    return snapshots.Select(snapshot => User.CreateFrom(snapshot)).FirstOrDefault();
                }
            });

            return user;
        }

        public async Task<User> GetUser(string username)
        {
            var user = await _dbRetryHandler.QueryAsync(async connection =>
            {
                using (var reader = await connection.QueryMultipleAsync("User_GetByUsername", new
                {
                    username
                }))
                {
                    var snapshots = reader.Read<UserSnapshot>().ToList();

                    return snapshots.Select(snapshot => User.CreateFrom(snapshot)).FirstOrDefault();
                }
            });

            return user;
        }

        /// <summary>
        /// Internal function to update super admin's tokens
        /// </summary>
        /// <param name="userId">The id of the super admin</param>
        /// <param name="newToken">The new token (null to revoke)</param>
        /// <returns></returns>
        public async Task<bool> UpdateToken(int userId, string newToken)
        {
            var result = await _dbRetryHandler.QueryAsync(async connection =>
            {
                return await connection.ExecuteAsync("User_Token", new
                {
                    userId,
                    Token = newToken
                });
            });

            return result > 0;
        }

        public async Task<List<User>> GetUsers(int branchId)
        {
            var users = await _dbRetryHandler.QueryAsync(async connection =>
            {
                using (var reader = await connection.QueryMultipleAsync("User_GetByBranchId", new
                {
                    branchId
                }))
                {
                    var snapshots = reader.Read<UserSnapshot>().ToList();

                    return snapshots.Select(snapshot => User.CreateFrom(snapshot)).ToList();
                }
            });

            return users;
        }
    }
}
