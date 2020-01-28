using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Models.Snapshots;
using System;

namespace SmartELock.Core.Domain.Models
{
    public class KeyboxAsset
    {
        public int KeyboxAssetId { get; private set; }
        public string Uuid { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime UpdatedOn { get; private set; }

        private KeyboxAsset(KeyboxAssetCreateCommand command)
        {
            Uuid = command.Uuid;
        }

        private KeyboxAsset(KeyboxAssetSnapshot snapshot)
        {
            KeyboxAssetId = snapshot.KeyboxAssetId;
            Uuid = snapshot.Uuid;
            CreatedOn = snapshot.CreatedOn;
            UpdatedOn = snapshot.UpdatedOn;
        }

        public static KeyboxAsset CreateFrom(KeyboxAssetCreateCommand command)
        {
            return new KeyboxAsset(command);
        }

        public static KeyboxAsset CreateFrom(KeyboxAssetSnapshot snapshot)
        {
            return new KeyboxAsset(snapshot);
        }
    }
}
