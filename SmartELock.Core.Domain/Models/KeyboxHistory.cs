using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Models.Snapshots;
using System;

namespace SmartELock.Core.Domain.Models
{
    public class KeyboxHistory
    {
        public int KeyboxHistoryId { get; private set; }
        public int KeyboxId { get; private set; }
        public int UserId { get; private set; }
        public int? TmpUserId { get; private set; }
        public int PropertyId { get; private set; }
        public DateTime InOn { get; private set; }
        public DateTime? OutOn { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime UpdatedOn { get; private set; }

        public void SetInData(int userId, int propertyId, DateTime inOn)
        {
            UserId = userId;
            PropertyId = propertyId;
            InOn = inOn;
        }

        public void SetOutData(DateTime outOn)
        {
            OutOn = outOn;
        }

        private KeyboxHistory(KeyboxHistoryCommand command)
        {
            KeyboxId = command.KeyboxId;
        }

        private KeyboxHistory(KeyboxHistorySnapshot snapshot)
        {
            KeyboxHistoryId = snapshot.KeyboxHistoryId;
            KeyboxId = snapshot.KeyboxId;
            UserId = snapshot.UserId;
            TmpUserId = snapshot.TmpUserId;
            PropertyId = snapshot.PropertyId;
            InOn = snapshot.InOn;
            OutOn = snapshot.OutOn;
            CreatedOn = snapshot.CreatedOn;
            UpdatedOn = snapshot.UpdatedOn;
        }

        public static KeyboxHistory CreateFrom(KeyboxHistoryCommand command)
        {
            return new KeyboxHistory(command);
        }

        public static KeyboxHistory CreateFrom(KeyboxHistorySnapshot snapshot)
        {
            return new KeyboxHistory(snapshot);
        }
    }
}
