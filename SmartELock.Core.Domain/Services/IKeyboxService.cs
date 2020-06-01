using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Models.Commands.Base;
using SmartELock.Core.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Services
{
    public interface IKeyboxService
    {
        Task<int> RegisterKeybox(KeyboxCreateCommand command);
        Task<bool> UpdateKeybox(KeyboxUpdateCommand command);
        Task<bool> UpdateKeyboxPin(KeyboxUpdateCommand command);
        Task<bool> AssignTo(KeyboxAssignToCommand command);
        Task<Keybox> GetKeybox(KeyboxCommand command);
        Task<List<Keybox>> GetKeyboxes(User currentUser, int userId);
        Task<List<Keybox>> GetKeyboxesIUnlocked(int userId);
        Task<int> CreateKeyboxProperty(KeyboxPropertyCreateCommand command);
        Task<bool> UpdateKeyboxProperty(KeyboxPropertyUpdateCommand command);
        Task<bool> EndKeyboxProperty(KeyboxPropertyCommand command);
        Task<Property> GetKeyboxProperty(KeyboxPropertyCommand command);
        Task<bool> UnlockPermission(User currentUser, KeyboxCommand command);
        Task<bool> Unlock(User currentUser, KeyboxHistoryCommand command);
        Task<bool> Lock(KeyboxHistoryCommand command);
        Task<List<KeyboxHistory>> GetKeyboxHistories(KeyboxPropertyCommand command);
        Task<int> CreatePropertyFeedback(User currentUser, PropertyFeedbackCreateCommand command);
        Task<List<PropertyFeedback>> GetKeyboxPropertyFeedback(KeyboxPropertyCommand command);
        Task<List<ResProperty>> GetPropertyResource(int propertyId);
        Task<bool> AddPropertyResource(int propertyId, byte[] bytes, FileType fileType);
        Task<bool> UpdatePropertyResource(int propertyId, int resPropertyId, byte[] bytes, FileType fileType);
        Task<bool> DeletePropertyResource(int propertyId, int resPropertyId);
        Task<byte[]> GetPropertyResourceData(int resPropertyId);
    }
}