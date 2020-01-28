using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Validators;
using SmartELock.Core.Services.Validators.Permissions;
using SmartELock.Core.Services.Validators.Specifications;
using System.Collections.Generic;

namespace SmartELock.Core.Services.Validators
{
    public class UserCreateValidator : BaseCommandValidator<UserCreateCommand>
    {
        private readonly IUserRepository _userRepository;

        public UserCreateValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        protected override IList<ISpecification<UserCreateCommand>> GetSpecifications(UserCreateCommand command = null)
        {
            return new List<ISpecification<UserCreateCommand>>()
            {
                new HasPermissionToCreateUser(_userRepository),
                new UserUsernameMustBeUnique(_userRepository)
            };
        }
    }
}
