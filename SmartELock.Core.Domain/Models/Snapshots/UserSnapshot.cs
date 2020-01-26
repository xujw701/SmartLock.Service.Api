using System;

namespace SmartELock.Core.Domain.Models.Snapshots
{
    public class UserSnapshot
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public bool Individual { get; set; }
        public int UserRoleId { get; set; }
        public int? ResPortraitId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
