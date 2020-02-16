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
        private readonly IPropertyRepository _propertyRepository;

        private readonly ICommandValidator<KeyboxCreateCommand> _keyboxRegisterValidator;
        private readonly ICommandValidator<KeyboxAssignToCommand> _keyboxAssignToValidator;
        private readonly ICommandValidator<KeyboxPropertyCreateCommand> _keyboxPropertyCreateValidator;
        private readonly ICommandValidator<KeyboxPropertyUpdateCommand> _keyboxPropertyUpdateValidator;
        private readonly ICommandValidator<KeyboxPropertyDeleteCommand> _keyboxPropertyDeleteValidator;

        public KeyboxService(IKeyboxRepository keyboxRepository, IKeyboxAssetRepository keyboxAssetRepository, IPropertyRepository propertyRepository,
                             ICommandValidator<KeyboxCreateCommand> keyboxRegisterValidator,
                             ICommandValidator<KeyboxAssignToCommand> keyboxAssignToValidator,
                             ICommandValidator<KeyboxPropertyCreateCommand> keyboxPropertyCreateValidator,
                             ICommandValidator<KeyboxPropertyUpdateCommand> keyboxPropertyUpdateValidator,
                             ICommandValidator<KeyboxPropertyDeleteCommand> keyboxPropertyDeleteValidator)
        {
            _keyboxRepository = keyboxRepository;
            _keyboxAssetRepository = keyboxAssetRepository;
            _propertyRepository = propertyRepository;

            _keyboxRegisterValidator = keyboxRegisterValidator;
            _keyboxAssignToValidator = keyboxAssignToValidator;
            _keyboxPropertyCreateValidator = keyboxPropertyCreateValidator;
            _keyboxPropertyUpdateValidator = keyboxPropertyUpdateValidator;
            _keyboxPropertyDeleteValidator = keyboxPropertyDeleteValidator;
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

        public async Task<bool> AssignTo(KeyboxAssignToCommand command)
        {
            var validationResult = await _keyboxAssignToValidator.Validate(command);

            if (!validationResult.IsValid)
            {
                throw new DomainValidationException(validationResult.ErrorMessage, validationResult.ErrorCode);
            }

            var keybox = await _keyboxRepository.GetKeybox(command.KeyboxId);

            keybox.SetOwner(command.TargetUserId);

            return await _keyboxRepository.UpdateKeybox(keybox);
        }

        public async Task<int> CreateKeyboxProperty(KeyboxPropertyCreateCommand command)
        {
            var validationResult = await _keyboxPropertyCreateValidator.Validate(command);

            if (!validationResult.IsValid)
            {
                throw new DomainValidationException(validationResult.ErrorMessage, validationResult.ErrorCode);
            }

            // Create property
            var property = Property.CreateFrom(command);

            var propertyId = await _propertyRepository.CreateProperty(property);

            // Update keybox 
            var keybox = await _keyboxRepository.GetKeybox(command.KeyboxId);

            keybox.SetKeyboxData(propertyId, command.KeyboxName, keybox.BatteryLevel, keybox.Pin);

            var keyboxResult = await _keyboxRepository.UpdateKeybox(keybox);

            return propertyId;
        }

        public async Task<bool> UpdateKeyboxProperty(KeyboxPropertyUpdateCommand command)
        {
            var validationResult = await _keyboxPropertyUpdateValidator.Validate(command);

            if (!validationResult.IsValid)
            {
                throw new DomainValidationException(validationResult.ErrorMessage, validationResult.ErrorCode);
            }

            // Create property
            var property = Property.CreateFrom(command);

            var propertyResult = await _propertyRepository.UpdateProperty(property);

            // Update keybox 
            var keybox = await _keyboxRepository.GetKeybox(command.KeyboxId);

            keybox.SetKeyboxData(keybox.PropertyId, command.KeyboxName, keybox.BatteryLevel, keybox.Pin);

            var keyboxResult = await _keyboxRepository.UpdateKeybox(keybox);

            return propertyResult && keyboxResult;
        }

        public async Task<bool> EndKeyboxProperty(KeyboxPropertyDeleteCommand command)
        {
            var validationResult = await _keyboxPropertyDeleteValidator.Validate(command);

            if (!validationResult.IsValid)
            {
                throw new DomainValidationException(validationResult.ErrorMessage, validationResult.ErrorCode);
            }

            // End property
            var propertyResult = await _propertyRepository.EndProperty(command.PropertyId);

            // Update keybox 
            var keybox = await _keyboxRepository.GetKeybox(command.KeyboxId);

            keybox.SetKeyboxData(null, keybox.KeyboxName, keybox.BatteryLevel, keybox.Pin);

            var keyboxResult = await _keyboxRepository.UpdateKeybox(keybox);

            return propertyResult && keyboxResult;
        }
    }
}
