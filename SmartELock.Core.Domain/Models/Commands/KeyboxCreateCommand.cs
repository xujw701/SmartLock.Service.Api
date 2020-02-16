using SmartELock.Core.Domain.Models.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Models.Commands
{
    public class KeyboxCreateCommand : KeyboxCommand, IKeyboxCreateCommand, IKeyboxAssetCommand
    {
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public string KeyboxName { get; set; }
        public int BatteryLevel { get; set; }
        public string Pin { get; set; }
    }
}