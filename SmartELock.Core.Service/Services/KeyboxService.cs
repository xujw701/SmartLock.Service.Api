using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Models.Exceptions;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Services;
using SmartELock.Core.Services.Validators;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartELock.Core.Services.Services
{
    public class KeyboxService : IKeyboxService
    {
        private readonly IKeyboxRepository _keyboxRepository;
        private readonly IKeyboxAssetRepository _keyboxAssetRepository;

        private readonly ICommandValidator<KeyboxCreateCommand> _keyboxRegisterValidator;

        public KeyboxService(IKeyboxRepository keyboxRepository, IKeyboxAssetRepository keyboxAssetRepository, ICommandValidator<KeyboxCreateCommand> keyboxRegisterValidator)
        {
            _keyboxRepository = keyboxRepository;
            _keyboxAssetRepository = keyboxAssetRepository;

            _keyboxRegisterValidator = keyboxRegisterValidator;
        }

        public async Task<int> RegisterKeybox(KeyboxCreateCommand command)
        {
            var validationResult = await _keyboxRegisterValidator.Validate(command);

            if (!validationResult.IsValid)
            {
                throw new DomainValidationException(validationResult.ErrorMessage, validationResult.ErrorCode);
            }

            var keybox = Keybox.CreateFrom(command);

            var keyboxAsset = await _keyboxAssetRepository.GetKeyboxAssetByUuid(keybox.Uuid);
            keybox.SetKeyboxAssetId(keyboxAsset.KeyboxAssetId);

            return await _keyboxRepository.CreateKeybox(keybox);
        }
    }
}
