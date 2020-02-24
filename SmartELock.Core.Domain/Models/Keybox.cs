using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Models.Snapshots;
using System;

namespace SmartELock.Core.Domain.Models
{
    public class Keybox
    {
        public int KeyboxId { get; private set; }
        public int CompanyId { get; private set; }
        public int BranchId { get; private set; }
        public int KeyboxAssetId { get; private set; }
        public string Uuid { get; private set; }
        public int? PropertyId { get; private set; }
        public string Address { get; private set; }
        public int? UserId { get; private set; }
        public string KeyboxName { get; private set; }
        public int BatteryLevel { get; private set; }
        public string Pin { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime UpdatedOn { get; private set; }

        private Keybox(KeyboxCreateCommand command)
        {
            CompanyId = command.CompanyId;
            BranchId = command.BranchId;
            Uuid = command.Uuid;
            KeyboxName = command.KeyboxName;
            BatteryLevel = command.BatteryLevel;
            Pin = command.Pin;
        }

        private Keybox(KeyboxSnapshot snapshot)
        {
            KeyboxId = snapshot.KeyboxId;
            CompanyId = snapshot.CompanyId;
            BranchId = snapshot.BranchId;
            KeyboxAssetId = snapshot.KeyboxAssetId;
            Uuid = snapshot.Uuid;
            PropertyId = snapshot.PropertyId;
            Address = snapshot.Address;
            UserId = snapshot.UserId;
            KeyboxName = snapshot.KeyboxName;
            BatteryLevel = snapshot.BatteryLevel;
            Pin = snapshot.Pin;
            CreatedOn = snapshot.CreatedOn;
            UpdatedOn = snapshot.UpdatedOn;
        }

        public void SetKeyboxAssetId(int keyboxAssetId)
        {
            KeyboxAssetId = keyboxAssetId;
        }

        public void SetOwner(int userId)
        {
            UserId = userId;
        }

        public void SetKeyboxData(int? propertyId, string keyboxName, int batteryLevel, string pin)
        {
            PropertyId = propertyId;
            KeyboxName = keyboxName;
            BatteryLevel = batteryLevel;
            Pin = pin;
        }

        public static Keybox CreateFrom(KeyboxCreateCommand command)
        {
            return new Keybox(command);
        }

        public static Keybox CreateFrom(KeyboxSnapshot snapshot)
        {
            return new Keybox(snapshot);
        }
    }
}
