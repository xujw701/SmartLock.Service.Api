using System;

namespace SmartELock.Core.Domain.Models.Snapshots
{
    public class KeyboxHistorySnapshot
    {
        public int KeyboxHistoryId { get; set; }
        public int KeyboxId { get; set; }
        public int UserId { get; set; }
        public int? TmpUserId { get; set; }
        public int PropertyId { get; set; }
        public DateTime InOn { get; set; }
        public DateTime? OutOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
