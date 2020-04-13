using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Validators;
using SmartELock.Core.Services.Validators.Permissions;
using SmartELock.Core.Services.Validators.Specifications;
using System.Collections.Generic;

namespace SmartELock.Core.Services.Validators
{
    public class KeyboxUpdateValidator : BaseCommandValidator<KeyboxUpdateCommand>
    {
        private readonly IKeyboxRepository _keyboxRepository;
        private readonly IUserRepository _userRepository;

        public KeyboxUpdateValidator(IKeyboxRepository keyboxRepository, IUserRepository userRepository)
        {
            _keyboxRepository = keyboxRepository;
            _userRepository = userRepository;
        }

        protected override IList<ISpecification<KeyboxUpdateCommand>> GetSpecifications(KeyboxUpdateCommand command = null)
        {
            return new List<ISpecification<KeyboxUpdateCommand>>()
            {
                new HasPermissionToUpdateKeybox(_keyboxRepository, _userRepository)
            };
        }
    }
}
