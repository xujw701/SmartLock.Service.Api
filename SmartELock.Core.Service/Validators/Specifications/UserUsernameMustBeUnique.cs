using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Exceptions;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Validators;
using System.Threading.Tasks;

namespace SmartELock.Core.Services.Validators.Specifications
{
    public class UserUsernameMustBeUnique : ISpecification<IUserCreateCommand>
    {
        private readonly IUserRepository _userRepository;

        public UserUsernameMustBeUnique(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> IsSatisfiedByAsync(IUserCreateCommand command)
        {
            var user = await _userRepository.GetUser(command.Username);

            var allow = user == null;

            return allow;
        }

        public string ErrorMessage(IUserCreateCommand obj)
        {
            return "User username must be unique";
        }

        public ErrorCode ErrorCode { get; } = ErrorCode.FieldMustUnique;
    }
}
