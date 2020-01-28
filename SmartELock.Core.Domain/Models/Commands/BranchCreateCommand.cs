using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Models.Commands
{
    public class BranchCreateCommand : IBranchCreateCommand
    {
        public int CompanyId { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }

        public int? OperatedBy { get; set; }
        public int? OperatedByAdmin { get; set; }
    }
}
