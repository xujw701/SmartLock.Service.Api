using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Validators;
using SmartELock.Core.Services.Validators.Permissions;
using SmartELock.Core.Services.Validators.Specifications;
using System.Collections.Generic;

namespace SmartELock.Core.Services.Validators
{
    public class KeyboxPropertyUpdateValidator : BaseCommandValidator<KeyboxPropertyUpdateCommand>
    {
        private readonly IKeyboxRepository _keyboxRepository;
        private readonly IUserRepository _userRepository;

        public KeyboxPropertyUpdateValidator(IKeyboxRepository keyboxRepository, IUserRepository userRepository)
        {
            _keyboxRepository = keyboxRepository;
            _userRepository = userRepository;
        }

        protected override IList<ISpecification<KeyboxPropertyUpdateCommand>> GetSpecifications(KeyboxPropertyUpdateCommand command = null)
        {
            return new List<ISpecification<KeyboxPropertyUpdateCommand>>()
            {
                new HasPermissionToOperateKeyboxProperty(_keyboxRepository, _userRepository),
                new KeyboxListed(_keyboxRepository)
            };
        }
    }
}
