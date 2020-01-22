using System;

namespace SmartELock.Core.Domain.Models.Snapshots
{
    public class CompanySnapshot
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
