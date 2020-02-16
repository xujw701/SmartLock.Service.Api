using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Validators;
using SmartELock.Core.Services.Validators.Permissions;
using SmartELock.Core.Services.Validators.Specifications;
using System.Collections.Generic;

namespace SmartELock.Core.Services.Validators
{
    public class KeyboxAssignToValidator : BaseCommandValidator<KeyboxAssignToCommand>
    {
        private readonly IKeyboxRepository _keyboxRepository;
        private readonly IUserRepository _userRepository;

        public KeyboxAssignToValidator(IKeyboxRepository keyboxRepository, IUserRepository userRepository)
        {
            _keyboxRepository = keyboxRepository;
            _userRepository = userRepository;
        }

        protected override IList<ISpecification<KeyboxAssignToCommand>> GetSpecifications(KeyboxAssignToCommand command = null)
        {
            return new List<ISpecification<KeyboxAssignToCommand>>()
            {
                new HasPermissionToAssignKeybox(_keyboxRepository, _userRepository)
            };
        }
    }
}
