using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Validators;
using SmartELock.Core.Services.Validators.Permissions;
using SmartELock.Core.Services.Validators.Specifications;
using System.Collections.Generic;

namespace SmartELock.Core.Services.Validators
{
    public class KeyboxPropertyCreateValidator : BaseCommandValidator<KeyboxPropertyCreateCommand>
    {
        private readonly IKeyboxRepository _keyboxRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPropertyRepository _propertyRepository;

        public KeyboxPropertyCreateValidator(IKeyboxRepository keyboxRepository, IUserRepository userRepository, IPropertyRepository propertyRepository)
        {
            _keyboxRepository = keyboxRepository;
            _userRepository = userRepository;
            _propertyRepository = propertyRepository;
        }

        protected override IList<ISpecification<KeyboxPropertyCreateCommand>> GetSpecifications(KeyboxPropertyCreateCommand command = null)
        {
            return new List<ISpecification<KeyboxPropertyCreateCommand>>()
            {
                new HasPermissionToOperateKeyboxProperty(_keyboxRepository, _userRepository),
                new KeyboxCanList(_keyboxRepository, _propertyRepository)
            };
        }
    }
}
