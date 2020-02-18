using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Models.Commands.Base;
using SmartELock.Core.Domain.Models.Exceptions;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Services;
using SmartELock.Core.Services.Validators;
using System;
using System.Collections.Generic;
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
        private readonly ICommandValidator<KeyboxPropertyCommand> _keyboxPropertyOperateValidator;
        private readonly ICommandValidator<KeyboxCommand> _keyboxAccessValidator;

        public KeyboxService(IKeyboxRepository keyboxRepository, IKeyboxAssetRepository keyboxAssetRepository, IPropertyRepository propertyRepository,
                             ICommandValidator<KeyboxCreateCommand> keyboxRegisterValidator,
                             ICommandValidator<KeyboxAssignToCommand> keyboxAssignToValidator,
                             ICommandValidator<KeyboxPropertyCreateCommand> keyboxPropertyCreateValidator,
                             ICommandValidator<KeyboxPropertyUpdateCommand> keyboxPropertyUpdateValidator,
                             ICommandValidator<KeyboxPropertyCommand> keyboxPropertyOperateValidator,
                             ICommandValidator<KeyboxCommand> keyboxAccessValidator)
        {
            _keyboxRepository = keyboxRepository;
            _keyboxAssetRepository = keyboxAssetRepository;
            _propertyRepository = propertyRepository;

            _keyboxRegisterValidator = keyboxRegisterValidator;
            _keyboxAssignToValidator = keyboxAssignToValidator;
            _keyboxPropertyCreateValidator = keyboxPropertyCreateValidator;
            _keyboxPropertyUpdateValidator = keyboxPropertyUpdateValidator;
            _keyboxPropertyOperateValidator = keyboxPropertyOperateValidator;
            _keyboxAccessValidator = keyboxAccessValidator;
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

        public async Task<Keybox> GetKeybox(KeyboxCommand command)
        {
            var validationResult = await _keyboxAccessValidator.Validate(command);

            if (!validationResult.IsValid)
            {
                throw new DomainValidationException(validationResult.ErrorMessage, validationResult.ErrorCode);
            }

            if (command.KeyboxId > 0)
            {
                return await _keyboxRepository.GetKeybox(command.KeyboxId);
            }
            else
            {
                return await _keyboxRepository.GetKeyboxByUuid(command.Uuid);
            }
        }

        public async Task<List<Keybox>> GetMyKeyboxes(int userId)
        {
            return await _keyboxRepository.GetKeyboxesByUserId(userId);
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

        public async Task<bool> EndKeyboxProperty(KeyboxPropertyCommand command)
        {
            var validationResult = await _keyboxPropertyOperateValidator.Validate(command);

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

        public async Task<Property> GetKeyboxProperty(KeyboxPropertyCommand command)
        {
            var validationResult = await _keyboxAccessValidator.Validate(command);

            if (!validationResult.IsValid)
            {
                throw new DomainValidationException(validationResult.ErrorMessage, validationResult.ErrorCode);
            }

            if (command.PropertyId > 0)
            {
                return await _propertyRepository.GetProperty(command.PropertyId);
            }

            return null;
        }

        public async Task<bool> Unlock(KeyboxHistoryCommand command)
        {
            var validationResult = await _keyboxAccessValidator.Validate(command);

            if (!validationResult.IsValid)
            {
                throw new DomainValidationException(validationResult.ErrorMessage, validationResult.ErrorCode);
            }

            var keyboxHistory = KeyboxHistory.CreateFrom(command);

            // Get keybox 
            var keybox = await _keyboxRepository.GetKeybox(command.KeyboxId);

            // Validate data
            if (keybox == null) return false;
            if (!command.OperatedBy.HasValue || !keybox.PropertyId.HasValue) return false;

            keyboxHistory.SetInData(command.OperatedBy.Value, keybox.PropertyId.Value, command.DateTime);

            var id = await _keyboxRepository.CreateKeyboxHistory(keyboxHistory);

            return id > 0;
        }

        public async Task<bool> Lock(KeyboxHistoryCommand command)
        {
            var validationResult = await _keyboxAccessValidator.Validate(command);

            if (!validationResult.IsValid)
            {
                throw new DomainValidationException(validationResult.ErrorMessage, validationResult.ErrorCode);
            }

            // Get keybox 
            var keybox = await _keyboxRepository.GetKeybox(command.KeyboxId);

            // Validate data
            if (keybox == null) return false;
            if (!command.OperatedBy.HasValue || !keybox.PropertyId.HasValue) return false;

            var unlockedHistories = await _keyboxRepository.GetUnlockedKeyboxHistories(command.KeyboxId, command.OperatedBy.Value, keybox.PropertyId.Value);
            var latestUnlockedHistory = unlockedHistories.FirstOrDefault();

            if (latestUnlockedHistory != null)
            {
                if (latestUnlockedHistory.InOn.CompareTo(command.DateTime) > 0)
                {
                    throw new DomainValidationException("OutOn is ealier than InOn", ErrorCode.UnknownError);
                }
                latestUnlockedHistory.SetOutData(command.DateTime);
                return await _keyboxRepository.UpdateKeyboxHistory(latestUnlockedHistory);
            }

            return false;
        }

        public async Task<List<KeyboxHistory>> GetKeyboxHistories(KeyboxPropertyCommand command)
        {
            var validationResult = await _keyboxAccessValidator.Validate(command);

            if (!validationResult.IsValid)
            {
                throw new DomainValidationException(validationResult.ErrorMessage, validationResult.ErrorCode);
            }

            if (command.KeyboxId > 0)
            {
                return await _keyboxRepository.GetKeyboxHistories(command.KeyboxId, command.PropertyId);
            }

            return null;
        }

        public async Task<int> CreatePropertyFeedback(PropertyFeedbackCreateCommand command)
        {
            var validationResult = await _keyboxAccessValidator.Validate(command);

            if (!validationResult.IsValid)
            {
                throw new DomainValidationException(validationResult.ErrorMessage, validationResult.ErrorCode);
            }

            var propertyFeedback = PropertyFeedback.CreateFrom(command);

            var propertyFeedbackId = await _propertyRepository.CreatePropertyFeedback(propertyFeedback);

            return propertyFeedbackId;
        }

        public async Task<List<PropertyFeedback>> GetKeyboxPropertyFeedback(KeyboxPropertyCommand command)
        {
            var validationResult = await _keyboxPropertyOperateValidator.Validate(command);

            if (!validationResult.IsValid)
            {
                throw new DomainValidationException(validationResult.ErrorMessage, validationResult.ErrorCode);
            }

            if (command.PropertyId > 0)
            {
                return await _propertyRepository.GetPropertyFeedback(command.PropertyId);
            }

            return null;
        }
    }
}