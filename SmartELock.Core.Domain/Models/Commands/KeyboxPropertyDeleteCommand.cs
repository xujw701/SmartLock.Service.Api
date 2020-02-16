using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Models.Commands
{
    public class KeyboxPropertyDeleteCommand : IKeyboxPropertyCommand
    {
        public int KeyboxId { get; set; }
        public int PropertyId { get; set; }

        public int? OperatedBy { get; set; }
        public int? OperatedByAdmin { get; set; }
    }
}
