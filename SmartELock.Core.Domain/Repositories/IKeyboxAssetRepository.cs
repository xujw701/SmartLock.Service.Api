using SmartELock.Core.Domain.Models;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Repositories
{
    public interface IKeyboxAssetRepository
    {
        Task<int> CreateKeyboxAsset(KeyboxAsset keyboxAsset);
        Task<KeyboxAsset> GetKeyboxAssetByUuid(string uuid);
    }
}
