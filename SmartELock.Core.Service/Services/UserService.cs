using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Services;
using System.Threading.Tasks;

namespace SmartELock.Core.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> CreateUser(UserCreateCommand command)
        {
            var user = User.CreateFrom(command);

            return await _userRepository.CreateUser(user);
        }
    }
}
