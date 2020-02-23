﻿using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Models.Commands.Base;
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
        Task<bool> AssignTo(KeyboxAssignToCommand command);
        Task<Keybox> GetKeybox(KeyboxCommand command);
        Task<List<Keybox>> GetMyKeyboxes(int userId);
        Task<int> CreateKeyboxProperty(KeyboxPropertyCreateCommand command);
        Task<bool> UpdateKeyboxProperty(KeyboxPropertyUpdateCommand command);
        Task<bool> EndKeyboxProperty(KeyboxPropertyCommand command);
        Task<Property> GetKeyboxProperty(KeyboxPropertyCommand command);
        Task<bool> Unlock(User currentUser, KeyboxHistoryCommand command);
        Task<bool> Lock(KeyboxHistoryCommand command);
        Task<List<KeyboxHistory>> GetKeyboxHistories(KeyboxPropertyCommand command);
        Task<int> CreatePropertyFeedback(PropertyFeedbackCreateCommand command);
        Task<List<PropertyFeedback>> GetKeyboxPropertyFeedback(KeyboxPropertyCommand command);
    }
}