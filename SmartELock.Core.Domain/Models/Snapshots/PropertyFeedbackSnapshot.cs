using System;

namespace SmartELock.Core.Domain.Models.Snapshots
{
    public class PropertyFeedbackSnapshot
    {
        public int PropertyFeedbackId { get; set; }
        public int PropertyId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; private set; }
        public int? ResPortraitId { get; private set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
