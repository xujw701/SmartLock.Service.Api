using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Models.Commands.Base;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Validators;
using SmartELock.Core.Services.Validators.Permissions;
using SmartELock.Core.Services.Validators.Specifications;
using System.Collections.Generic;

namespace SmartELock.Core.Services.Validators
{
    public class KeyboxAccessValidator : BaseCommandValidator<KeyboxCommand>
    {
        private readonly IKeyboxRepository _keyboxRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPropertyRepository _propertyRepository;

        public KeyboxAccessValidator(IKeyboxRepository keyboxRepository, IUserRepository userRepository, IPropertyRepository propertyRepository)
        {
            _keyboxRepository = keyboxRepository;
            _userRepository = userRepository;
            _propertyRepository = propertyRepository;
        }

        protected override IList<ISpecification<KeyboxCommand>> GetSpecifications(KeyboxCommand command = null)
        {
            return new List<ISpecification<KeyboxCommand>>()
            {
                new HasPermissionToAccessKeybox(_keyboxRepository, _userRepository)
            };
        }
    }
}
