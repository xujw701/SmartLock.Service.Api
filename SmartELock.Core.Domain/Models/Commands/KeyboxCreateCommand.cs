using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Models.Commands
{
    public class KeyboxCreateCommand : IKeyboxCreateCommand
    {
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public string Uuid { get; set; }
        public string KeyboxName { get; set; }
        public int BatteryLevel { get; set; }
        public string Pin { get; set; }

        public int? OperatedBy { get; set; }
        public int? OperatedByAdmin { get; set; }
    }
}