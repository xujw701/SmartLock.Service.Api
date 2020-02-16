using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Commands;
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
        Task<int> CreateKeyboxProperty(KeyboxPropertyCreateCommand command);
        Task<bool> UpdateKeyboxProperty(KeyboxPropertyUpdateCommand command);
        Task<bool> EndKeyboxProperty(KeyboxPropertyDeleteCommand command);
    }
}