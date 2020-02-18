using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Validators;
using SmartELock.Core.Services.Validators.Permissions;
using SmartELock.Core.Services.Validators.Specifications;
using System.Collections.Generic;

namespace SmartELock.Core.Services.Validators
{
    public class KeyboxPropertyOperateValidator : BaseCommandValidator<KeyboxPropertyCommand>
    {
        private readonly IKeyboxRepository _keyboxRepository;
        private readonly IUserRepository _userRepository;

        public KeyboxPropertyOperateValidator(IKeyboxRepository keyboxRepository, IUserRepository userRepository)
        {
            _keyboxRepository = keyboxRepository;
            _userRepository = userRepository;
        }

        protected override IList<ISpecification<KeyboxPropertyCommand>> GetSpecifications(KeyboxPropertyCommand command = null)
        {
            return new List<ISpecification<KeyboxPropertyCommand>>()
            {
                new HasPermissionToOperateKeyboxProperty(_keyboxRepository, _userRepository)
            };
        }
    }
}
