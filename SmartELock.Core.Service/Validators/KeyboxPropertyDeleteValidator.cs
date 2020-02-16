using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Validators;
using SmartELock.Core.Services.Validators.Permissions;
using SmartELock.Core.Services.Validators.Specifications;
using System.Collections.Generic;

namespace SmartELock.Core.Services.Validators
{
    public class KeyboxPropertyDeleteValidator : BaseCommandValidator<KeyboxPropertyDeleteCommand>
    {
        private readonly IKeyboxRepository _keyboxRepository;
        private readonly IUserRepository _userRepository;

        public KeyboxPropertyDeleteValidator(IKeyboxRepository keyboxRepository, IUserRepository userRepository)
        {
            _keyboxRepository = keyboxRepository;
            _userRepository = userRepository;
        }

        protected override IList<ISpecification<KeyboxPropertyDeleteCommand>> GetSpecifications(KeyboxPropertyDeleteCommand command = null)
        {
            return new List<ISpecification<KeyboxPropertyDeleteCommand>>()
            {
                new HasPermissionToDeleteKeyboxProperty(_keyboxRepository, _userRepository)
            };
        }
    }
}
