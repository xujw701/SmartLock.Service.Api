using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Validators;
using SmartELock.Core.Services.Validators.Permissions;
using SmartELock.Core.Services.Validators.Specifications;
using System.Collections.Generic;

namespace SmartELock.Core.Services.Validators
{
    public class KeyboxHistoryValidator : BaseCommandValidator<KeyboxHistoryCommand>
    {
        private readonly IKeyboxRepository _keyboxRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPropertyRepository _propertyRepository;

        public KeyboxHistoryValidator(IKeyboxRepository keyboxRepository, IUserRepository userRepository, IPropertyRepository propertyRepository)
        {
            _keyboxRepository = keyboxRepository;
            _userRepository = userRepository;
            _propertyRepository = propertyRepository;
        }

        protected override IList<ISpecification<KeyboxHistoryCommand>> GetSpecifications(KeyboxHistoryCommand command = null)
        {
            return new List<ISpecification<KeyboxHistoryCommand>>()
            {
                new HasPermissionToOperateKeyboxHistory(_keyboxRepository, _userRepository)
            };
        }
    }
}
