using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Services;
using System;
using System.Threading.Tasks;

namespace SmartELock.Core.Services.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly ISuperAdminService _superAdminService;
        private readonly IUserService _userService;

        public AuthorizationService(ISuperAdminService superAdminService, IUserService userService)
        {
            _superAdminService = superAdminService;
            _userService = userService;
        }

        public async Task<Tuple<bool, SuperAdmin>> CheckAdminToken(int superAdminId, string token)
        {
            return await _superAdminService.CheckToken(superAdminId, token);
        }

        public async Task<Tuple<bool, User>> CheckUserToken(int userId, string token)
        {
            return await _userService.CheckToken(userId, token);
        }
    }
}
