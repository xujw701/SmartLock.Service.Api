﻿using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Exceptions;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Validators;
using System.Threading.Tasks;

namespace SmartELock.Core.Services.Validators.Specifications
{
    public class KeyboxAssetUuidMustExist : ISpecification<IKeyboxAssetCommand>
    {
        private readonly IKeyboxAssetRepository _keyboxAssetRepository;

        public KeyboxAssetUuidMustExist(IKeyboxAssetRepository keyboxAssetRepository)
        {
            _keyboxAssetRepository = keyboxAssetRepository;
        }

        public async Task<bool> IsSatisfiedByAsync(IKeyboxAssetCommand command)
        {
            var keyboxAsset = await _keyboxAssetRepository.GetKeyboxAssetByUuid(command.Uuid);

            var allow = keyboxAsset != null;

            return allow;
        }

        public string ErrorMessage(IKeyboxAssetCommand obj)
        {
            return "Uuid must exist";
        }

        public ErrorCode ErrorCode { get; } = ErrorCode.FieldMustUnique;
    }
}
