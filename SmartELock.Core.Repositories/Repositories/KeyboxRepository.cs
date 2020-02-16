using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Snapshots;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Repositories.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace SmartELock.Core.Repositories.Repositories
{
    public class KeyboxRepository : IKeyboxRepository
    {
        private readonly IDbRetryHandler _dbRetryHandler;

        public KeyboxRepository(IDbRetryHandler dbRetryHandler)
        {
            _dbRetryHandler = dbRetryHandler;
        }

        public async Task<int> CreateKeybox(Keybox keybox)
        {
            var id = await _dbRetryHandler.QueryAsync(async connection =>
            {
                using (var reader = await connection.QueryMultipleAsync("Keybox_Create", new
                {
                    keybox.CompanyId,
                    keybox.BranchId,
                    keybox.KeyboxAssetId,
                    keybox.Uuid,
                    keybox.UserId,
                    keybox.KeyboxName,
                    keybox.BatteryLevel,
                    keybox.Pin
                }))
                {
                    return reader.Read<int>().Single();
                }
            });

            return id;
        }

        public async Task<bool> UpdateKeybox(Keybox keybox)
        {
            var result = await _dbRetryHandler.QueryAsync(async connection =>
            {
                return await connection.ExecuteAsync("Keybox_Update", new
                {
                    keybox.KeyboxId,
                    keybox.CompanyId,
                    keybox.BranchId,
                    keybox.KeyboxAssetId,
                    keybox.Uuid,
                    keybox.PropertyId,
                    keybox.UserId,
                    keybox.KeyboxName,
                    keybox.BatteryLevel,
                    keybox.Pin
                });
            });

            return result > 0;
        }

        public async Task<Keybox> GetKeyboxByUuid(string uuid)
        {
            var keybox = await _dbRetryHandler.QueryAsync(async connection =>
            {
                using (var reader = await connection.QueryMultipleAsync("Keybox_GetByUuid", new
                {
                    uuid
                }))
                {
                    var snapshots = reader.Read<KeyboxSnapshot>().ToList();

                    return snapshots.Select(snapshot => Keybox.CreateFrom(snapshot)).FirstOrDefault();
                }
            });

            return keybox;
        }

        public async Task<Keybox> GetKeybox(int keyboxId)
        {
            var keybox = await _dbRetryHandler.QueryAsync(async connection =>
            {
                using (var reader = await connection.QueryMultipleAsync("Keybox_Get", new
                {
                    keyboxId
                }))
                {
                    var snapshots = reader.Read<KeyboxSnapshot>().ToList();

                    return snapshots.Select(snapshot => Keybox.CreateFrom(snapshot)).FirstOrDefault();
                }
            });

            return keybox;
        }
    }
}
