using System;

namespace SmartELock.Core.Domain.Models.Snapshots
{
    public class KeyboxAssetSnapshot
    {
        public int KeyboxAssetId { get; set; }
        public string Uuid { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
