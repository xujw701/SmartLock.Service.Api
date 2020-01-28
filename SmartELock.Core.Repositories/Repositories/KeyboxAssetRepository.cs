using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Snapshots;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Repositories.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace SmartELock.Core.Repositories.Repositories
{
    public class KeyboxAssetRepository : IKeyboxAssetRepository
    {
        private readonly IDbRetryHandler _dbRetryHandler;

        public KeyboxAssetRepository(IDbRetryHandler dbRetryHandler)
        {
            _dbRetryHandler = dbRetryHandler;
        }

        public async Task<int> CreateKeyboxAsset(KeyboxAsset keyboxAsset)
        {
            var id = await _dbRetryHandler.QueryAsync(async connection =>
            {
                using (var reader = await connection.QueryMultipleAsync("KeyboxAsset_Create", new
                {
                    keyboxAsset.Uuid
                }))
                {
                    return reader.Read<int>().Single();
                }
            });

            return id;
        }

        public async Task<KeyboxAsset> GetKeyboxAssetByUuid(string uuid)
        {
            var keyboxAsset = await _dbRetryHandler.QueryAsync(async connection =>
            {
                using (var reader = await connection.QueryMultipleAsync("KeyboxAsset_GetByUuid", new
                {
                    uuid
                }))
                {
                    var snapshots = reader.Read<KeyboxAssetSnapshot>().ToList();

                    return snapshots.Select(snapshot => KeyboxAsset.CreateFrom(snapshot)).FirstOrDefault();
                }
            });

            return keyboxAsset;
        }
    }
}
