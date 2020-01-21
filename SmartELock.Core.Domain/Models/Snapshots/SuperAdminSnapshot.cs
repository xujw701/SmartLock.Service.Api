using System;

namespace SmartELock.Core.Domain.Models.Snapshots
{
    public class SuperAdminSnapshot
    {
        public int SuperAdminId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
