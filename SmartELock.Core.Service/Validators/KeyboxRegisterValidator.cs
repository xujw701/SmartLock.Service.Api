using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Validators;
using SmartELock.Core.Services.Validators.Specifications;
using System.Collections.Generic;

namespace SmartELock.Core.Services.Validators
{
    public class KeyboxRegisterValidator : BaseCommandValidator<KeyboxCreateCommand>
    {
        private readonly IKeyboxAssetRepository _keyboxAssetRepository;
        private readonly IKeyboxRepository _keyboxRepository;

        public KeyboxRegisterValidator(IKeyboxAssetRepository keyboxAssetRepository, IKeyboxRepository keyboxRepository)
        {
            _keyboxAssetRepository = keyboxAssetRepository;
            _keyboxRepository = keyboxRepository;
        }

        protected override IList<ISpecification<KeyboxCreateCommand>> GetSpecifications(KeyboxCreateCommand command = null)
        {
            return new List<ISpecification<KeyboxCreateCommand>>()
            {
                new KeyboxAssetUuidMustExist(_keyboxAssetRepository),
                new KeyboxUuidMustBeUnique(_keyboxRepository)
            };
        }
    }
}
