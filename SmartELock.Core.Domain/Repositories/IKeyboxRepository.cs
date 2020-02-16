﻿using SmartELock.Core.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Repositories
{
    public interface IKeyboxRepository
    {
        Task<int> CreateKeybox(Keybox keybox);
        Task<bool> UpdateKeybox(Keybox keybox);
        Task<Keybox> GetKeyboxByUuid(string uuid);
        Task<Keybox> GetKeybox(int keyboxId);
        Task<int> CreateKeyboxHistory(KeyboxHistory keyboxHistory);
        Task<bool> UpdateKeyboxHistory(KeyboxHistory keyboxHistory);
        Task<List<KeyboxHistory>> GetUnlockedKeyboxHistories(int keyboxId, int userId, int propertyId, int? tmpUserId = null);
    }
}
