﻿using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Models.Commands.Base;
using SmartELock.Core.Domain.Models.Constants;
using SmartELock.Core.Domain.Models.Enums;
using SmartELock.Core.Domain.Models.Exceptions;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Services;
using SmartELock.Core.Services.Validators;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartELock.Core.Services.Services
{
    public class KeyboxService : IKeyboxService
    {
        private const int PropertyResourceLimit = 5;

        private readonly IKeyboxRepository _keyboxRepository;
        private readonly IKeyboxAssetRepository _keyboxAssetRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IResourceRepository _resourceRepository;

        private readonly IPushNotificationService _pushNotificationService;

        private readonly ICommandValidator<KeyboxCreateCommand> _keyboxRegisterValidator;
        private readonly ICommandValidator<KeyboxUpdateCommand> _keyboxUpdateValidator;
        private readonly ICommandValidator<KeyboxAssignToCommand> _keyboxAssignToValidator;
        private readonly ICommandValidator<KeyboxPropertyCreateCommand> _keyboxPropertyCreateValidator;
        private readonly ICommandValidator<KeyboxPropertyUpdateCommand> _keyboxPropertyUpdateValidator;
        private readonly ICommandValidator<KeyboxPropertyCommand> _keyboxPropertyOperateValidator;
        private readonly ICommandValidator<KeyboxCommand> _keyboxAccessValidator;

        public KeyboxService(IKeyboxRepository keyboxRepository, IKeyboxAssetRepository keyboxAssetRepository, IPropertyRepository propertyRepository, IUserRepository userRepository, IResourceRepository resourceRepository,
                             IPushNotificationService pushNotificationService,
                             ICommandValidator<KeyboxCreateCommand> keyboxRegisterValidator,
                             ICommandValidator<KeyboxUpdateCommand> keyboxUpdateValidator,
                             ICommandValidator<KeyboxAssignToCommand> keyboxAssignToValidator,
                             ICommandValidator<KeyboxPropertyCreateCommand> keyboxPropertyCreateValidator,
                             ICommandValidator<KeyboxPropertyUpdateCommand> keyboxPropertyUpdateValidator,
                             ICommandValidator<KeyboxPropertyCommand> keyboxPropertyOperateValidator,
                             ICommandValidator<KeyboxCommand> keyboxAccessValidator)
        {
            _keyboxRepository = keyboxRepository;
            _keyboxAssetRepository = keyboxAssetRepository;
            _propertyRepository = propertyRepository;
            _userRepository = userRepository;
            _resourceRepository = resourceRepository;

            _pushNotificationService = pushNotificationService;

            _keyboxRegisterValidator = keyboxRegisterValidator;
            _keyboxUpdateValidator = keyboxUpdateValidator;
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

        public async Task<List<Keybox>> GetKeyboxes(User currentUser, int userId)
        {
            if (currentUser.UserRoleId == UserRole.User)
            {
                if (currentUser.UserId != userId)
                {
                    throw new DomainValidationException("You must have permission to get keyboxes", ErrorCode.MustHasPermission);
                }
            }
            else
            {
                var user = await _userRepository.GetUser(userId);

                if (currentUser.UserRoleId <= UserRole.SalesManager)
                {
                    if (currentUser.BranchId != user.BranchId || currentUser.CompanyId != user.CompanyId)
                    {
                        throw new DomainValidationException("You must have permission to get keyboxes", ErrorCode.MustHasPermission);
                    }
                }
                else if (currentUser.UserRoleId <= UserRole.GeneralManagerer)
                {
                    if (currentUser.CompanyId != user.CompanyId)
                    {
                        throw new DomainValidationException("You must have permission to get keyboxes", ErrorCode.MustHasPermission);
                    }
                }
                else
                {
                    return new List<Keybox>();
                }
            }
            return await _keyboxRepository.GetKeyboxesByUserId(userId);
        }

        public async Task<bool> UpdateKeyboxPin(KeyboxUpdateCommand command)
        {
            var keybox = await _keyboxRepository.GetKeybox(command.KeyboxId);

            command.UserId = keybox.UserId != null? keybox.UserId.Value : 0;
            command.CompanyId = keybox.CompanyId;
            command.BranchId = keybox.BranchId;

            var validationResult = await _keyboxUpdateValidator.Validate(command);

            if (!validationResult.IsValid)
            {
                throw new DomainValidationException(validationResult.ErrorMessage, validationResult.ErrorCode);
            }

            // Update keybox
            keybox.SetKeyboxData(keybox.PropertyId, keybox.KeyboxName, keybox.BatteryLevel, command.Pin);

            var keyboxResult = await _keyboxRepository.UpdateKeybox(keybox);

            return keyboxResult;
        }

        public async Task<bool> UpdateKeybox(KeyboxUpdateCommand command)
        {
            var user = await _userRepository.GetUser(command.UserId);
            command.CompanyId = user.CompanyId;
            command.BranchId = user.BranchId;

            var validationResult = await _keyboxUpdateValidator.Validate(command);

            if (!validationResult.IsValid)
            {
                throw new DomainValidationException(validationResult.ErrorMessage, validationResult.ErrorCode);
            }

            // Update keybox
            var keybox = await _keyboxRepository.GetKeybox(command.KeyboxId);

            // End the property if it has
            if (command.UserId != keybox.UserId)
            {
                if (keybox.PropertyId.HasValue)
                {
                    var endKeyboxPropertyCommand = new KeyboxPropertyCommand()
                    {
                        KeyboxId = keybox.KeyboxId,
                        PropertyId = keybox.PropertyId.Value,
                        OperatedBy = command.OperatedBy,
                        OperatedByAdmin = command.OperatedByAdmin
                    };
                    var success = await EndKeyboxProperty(endKeyboxPropertyCommand);

                    // Refresh keybox data
                    if (success)
                    {
                        keybox = await _keyboxRepository.GetKeybox(command.KeyboxId);
                    }
                }
            }

            keybox.SetKeyboxData(command.CompanyId, command.BranchId, command.UserId, command.KeyboxName, keybox.Pin);
            
            var keyboxResult = await _keyboxRepository.UpdateKeybox(keybox);

            return keyboxResult;
        }

        public async Task<List<Keybox>> GetKeyboxesIUnlocked(int userId)
        {
            var keyboxes = await _keyboxRepository.GetKeyboxesExtraByUserId(userId);

            return keyboxes;
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

        public async Task<bool> UnlockPermission(User currentUser, KeyboxCommand command)
        {
            var validationResult = await _keyboxAccessValidator.Validate(command);

            if (!validationResult.IsValid)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Unlock(User currentUser, KeyboxHistoryCommand command)
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
            if (keybox == null)
            {
                return false;
            }
            if (!command.OperatedBy.HasValue || !keybox.PropertyId.HasValue)
            {
                // Owner can unlock keybox even it is not listed
                if (command.OperatedBy.HasValue && keybox.UserId.HasValue && command.OperatedBy.Value == keybox.UserId.Value) return true;

                return false;
            }

            keyboxHistory.SetInData(command.OperatedBy.Value, keybox.PropertyId.Value, command.DateTime);

            var id = await _keyboxRepository.CreateKeyboxHistory(keyboxHistory);

            // Not owner
            if (command.OperatedBy.HasValue && keybox.UserId.HasValue && command.OperatedBy.Value != keybox.UserId.Value)
            {
                if (currentUser != null)
                {
                    await _pushNotificationService.SendNotification("Your keybox has been unlocked",
                                                                    $"Keybox in {keybox.Address} was unlocked by {currentUser.FirstName} {currentUser.LastName}.",
                                                                    string.Empty,
                                                                    new []{ $"{keybox.UserId.Value}" });
                }
            }

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

        public async Task<int> CreatePropertyFeedback(User currentUser, PropertyFeedbackCreateCommand command)
        {
            var validationResult = await _keyboxAccessValidator.Validate(command);

            if (!validationResult.IsValid)
            {
                throw new DomainValidationException(validationResult.ErrorMessage, validationResult.ErrorCode);
            }

            var propertyFeedback = PropertyFeedback.CreateFrom(command);

            var propertyFeedbackId = await _propertyRepository.CreatePropertyFeedback(propertyFeedback);

            if (currentUser != null)
            {
                var keybox = await _keyboxRepository.GetKeybox(command.KeyboxId);
                await _pushNotificationService.SendNotification("Your got a new feedback",
                                                                $"A new feedback for {keybox.Address} was posted by {currentUser.FirstName} {currentUser.LastName}.",
                                                                string.Empty,
                                                                new[] { $"{keybox.UserId.Value}" });
            }

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

        public async Task<List<ResProperty>> GetPropertyResource(int propertyId)
        {
            var result = await _resourceRepository.GetResPropertyList(propertyId);

            return result;
        }

        public async Task<bool> AddPropertyResource(int propertyId, byte[] bytes, FileType fileType)
        {
            var resources = await _resourceRepository.GetResPropertyList(propertyId);

            if (resources.Count == PropertyResourceLimit)
            {
                throw new DomainValidationException("Reach property resource limit", ErrorCode.PropertyResourceLimit);
            }

            // Add to Azure storage
            var blobUrl = await _resourceRepository.SaveBlob(bytes, fileType, ResourceType.Property);

            var result = await _resourceRepository.AddPropertyResource(propertyId, blobUrl);

            return result > 0;
        }

        public async Task<bool> UpdatePropertyResource(int propertyId, int resPropertyId, byte[] bytes, FileType fileType)
        {
            var resources = await _resourceRepository.GetResPropertyList(propertyId);

            var oldResource = resources.FirstOrDefault(res => res.ResPropertyId == resPropertyId);

            if (oldResource != null)
            {
                // Remove from Azure storage
                var deleteBlobResult = await _resourceRepository.DeleteBlob(oldResource.Url, ResourceType.Property);

                // Add to Azure storage
                var blobUrl = await _resourceRepository.SaveBlob(bytes, fileType, ResourceType.Property);

                var result = await _resourceRepository.UpdateResProperty(resPropertyId, blobUrl);

                return result;
            }

            return false;
        }

        public async Task<bool> DeletePropertyResource(int propertyId, int resPropertyId)
        {
            var resources = await _resourceRepository.GetResPropertyList(propertyId);

            var oldResource = resources.FirstOrDefault(res => res.ResPropertyId == resPropertyId);

            if (oldResource != null)
            {
                // Remove from Azure storage
                var deleteBlobResult = await _resourceRepository.DeleteBlob(oldResource.Url, ResourceType.Property);

                var result = await _resourceRepository.DeleteResProperty(resPropertyId);

                return result;
            }

            return false;
        }

        public async Task<byte[]> GetPropertyResourceData(int resPropertyId)
        {
            var url = await _resourceRepository.GetResProperty(resPropertyId);
            if (url == null) return null;
            return await _resourceRepository.LoadBlob(url, ResourceType.Property);
        }
    }
}