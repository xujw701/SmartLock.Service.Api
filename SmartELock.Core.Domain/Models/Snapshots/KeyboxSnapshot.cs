using System;

namespace SmartELock.Core.Domain.Models.Snapshots
{
    public class KeyboxSnapshot
    {
        public int KeyboxId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int KeyboxAssetId { get; set; }
        public string Uuid { get; set; }
        public int? PropertyId { get; set; }
        public string Address { get; set; }
        public int? UserId { get; set; }
        public string KeyboxName { get; set; }
        public int BatteryLevel { get; set; }
        public string Pin { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
