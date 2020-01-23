using System;

namespace SmartELock.Core.Domain.Models.Snapshots
{
    public class BranchSnapshot
    {
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
