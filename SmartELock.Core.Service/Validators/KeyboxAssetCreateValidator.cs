using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Validators;
using SmartELock.Core.Services.Validators.Permissions;
using SmartELock.Core.Services.Validators.Specifications;
using System.Collections.Generic;

namespace SmartELock.Core.Services.Validators
{
    public class KeyboxAssetCreateValidator : BaseCommandValidator<KeyboxAssetCreateCommand>
    {
        private readonly IKeyboxAssetRepository _keyboxAssetRepository;

        public KeyboxAssetCreateValidator(IKeyboxAssetRepository keyboxAssetRepository)
        {
            _keyboxAssetRepository = keyboxAssetRepository;
        }

        protected override IList<ISpecification<KeyboxAssetCreateCommand>> GetSpecifications(KeyboxAssetCreateCommand command = null)
        {
            return new List<ISpecification<KeyboxAssetCreateCommand>>()
            {
                new KeyboxAssetUuidMustBeUnique(_keyboxAssetRepository)
            };
        }
    }
}
