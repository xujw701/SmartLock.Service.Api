using SmartELock.Core.Domain.Models;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Repositories
{
    public interface IKeyboxRepository
    {
        Task<int> CreateKeybox(Keybox keybox);
        Task<bool> UpdateKeybox(Keybox keybox);
        Task<Keybox> GetKeyboxByUuid(string uuid);
        Task<Keybox> GetKeybox(int keyboxId);
    }
}
