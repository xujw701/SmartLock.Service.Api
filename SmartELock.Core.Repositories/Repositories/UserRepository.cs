using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Snapshots;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Repositories.Infrastructure;
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
    }
}
