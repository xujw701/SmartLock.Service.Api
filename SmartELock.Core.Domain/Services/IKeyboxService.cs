using SmartELock.Core.Domain.Models;
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
        Task<bool> EndKeyboxProperty(KeyboxPropertyDeleteCommand command);
        Task<Property> GetKeyboxProperty(KeyboxPropertyGetCommand command);
        Task<bool> Unlock(KeyboxHistoryCommand command);
        Task<bool> Lock(KeyboxHistoryCommand command);
        Task<List<KeyboxHistory>> GetKeyboxHistories(KeyboxPropertyGetCommand command);
    }
}