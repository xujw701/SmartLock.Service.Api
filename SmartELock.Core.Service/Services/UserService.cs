using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Services;
using System;
using System.Linq;
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

        public async Task<User> Login(UserLoginCommand command)
        {
            var userId = await Auth(command);

            if (userId > 0)
            {
                var success = await IssueToken(userId);

                if (!success) return null;

                var user = await _userRepository.GetUser(userId);

                return user;
            }

            return null;
        }

        public async Task<bool> CheckToken(int userId, string token)
        {
            var user = await _userRepository.GetUser(userId);

            if (user == null || string.IsNullOrEmpty(token) || string.IsNullOrEmpty(user.Token)) return false;

            return user.Token.Equals(token);
        }

        private async Task<int> Auth(UserLoginCommand command)
        {
            var user = await _userRepository.GetUser(command.Username);

            if (user == null || string.IsNullOrEmpty(command.Password) || string.IsNullOrEmpty(command.Password)) return 0;

            if (command.Password.Equals(user.Password))
            {
                return user.UserId;
            }

            return 0;
        }

        private async Task<bool> IssueToken(int userId)
        {
            // Generate a token based on Now
            var time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            var key = Guid.NewGuid().ToByteArray();
            var token = Convert.ToBase64String(time.Concat(key).ToArray());

            // Get the timestamp of a token
            //var data = Convert.FromBase64String(token);
            //var when = DateTime.FromBinary(BitConverter.ToInt64(data, 0));

            return await _userRepository.UpdateToken(userId, token);
        }
    }
}