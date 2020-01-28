using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Models.Commands
{
    public class UserCreateCommand : IUserCreateCommand
    {
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Individual { get; set; }
        public int UserRoleId { get; set; }

        public int? OperatedBy { get; set; }
        public int? OperatedByAdmin { get; set; }
    }
}
