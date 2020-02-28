using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Models.Enums;
using SmartELock.Core.Domain.Models.Exceptions;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Services;
using SmartELock.Core.Services.Validators;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartELock.Core.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IResourceRepository _resourceRepository;

        private readonly ICommandValidator<UserCreateCommand> _userCreateValidator;

        public UserService(IUserRepository userRepository, IResourceRepository resourceRepository, ICommandValidator<UserCreateCommand> userCreateValidator)
        {
            _userRepository = userRepository;
            _resourceRepository = resourceRepository;

            _userCreateValidator = userCreateValidator;
        }

        public async Task<int> CreateUser(UserCreateCommand command)
        {
            var validationResult = await _userCreateValidator.Validate(command);

            if (!validationResult.IsValid)
            {
                throw new DomainValidationException(validationResult.ErrorMessage, validationResult.ErrorCode);
            }

            var user = User.CreateFrom(command);

            return await _userRepository.CreateUser(user);
        }

        public async Task<bool> UpdateMe(UserMeUpdateCommand command)
        {
            var user = await _userRepository.GetUser(command.UserId);

            if (string.IsNullOrEmpty(command.Password)) command.Password = user.Password;

            user.Update(command.FirstName, command.LastName, command.Email, command.Phone, command.Password);

            return await _userRepository.UpdateUser(user);
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

        public async Task<Tuple<bool, User>> CheckToken(int userId, string token)
        {
            var user = await _userRepository.GetUser(userId);

            if (user == null || string.IsNullOrEmpty(token) || string.IsNullOrEmpty(user.Token))
            {
                return new Tuple<bool, User>(false, null);
            }

            return new Tuple<bool, User>(user.Token.Equals(token), user);
        }

        public async Task<int> Auth(UserLoginCommand command)
        {
            var user = await _userRepository.GetUser(command.Username);

            if (user == null || string.IsNullOrEmpty(command.Password) || string.IsNullOrEmpty(command.Password)) return 0;

            if (command.Password.Equals(user.Password))
            {
                return user.UserId;
            }

            return 0;
        }

        public async Task<bool> UpdatePortrait(int userId, byte[] bytes, FileType fileType)
        {
            var user = await _userRepository.GetUser(userId);

            // Remove previous portrait
            if (user.ResPortraitId.HasValue)
            {
                var url = await _resourceRepository.GetPortrait(user.ResPortraitId.Value);

                // Remove from Azure storage
                var deleteBlobResult = await _resourceRepository.DeleteBlob(url, ResourceType.Portrait);
            }

            // Add to Azure storage
            var blobUrl = await _resourceRepository.SaveBlob(bytes, fileType, ResourceType.Portrait);

            if (user.ResPortraitId.HasValue)
            {
                return await _resourceRepository.UpdatePortrait(user.ResPortraitId.Value, blobUrl);
            }
            else
            {
                // Add to Database
                var portaitId = await _resourceRepository.AddPortrait(blobUrl);

                user.Update(portaitId);

                return await _userRepository.UpdateUser(user);
            }
        }

        public async Task<byte[]> GetPortrait(int portraitId)
        {
            var url = await _resourceRepository.GetPortrait(portraitId);
            if (url == null) return null;
            return await _resourceRepository.LoadBlob(url, ResourceType.Portrait);
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