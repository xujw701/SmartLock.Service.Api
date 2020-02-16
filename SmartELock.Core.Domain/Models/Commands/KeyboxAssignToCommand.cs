using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Models.Commands
{
    public class KeyboxAssignToCommand : IKeyboxAssignToCommand
    {
        public int KeyboxId { get; set; }
        public int TargetUserId { get; set; }

        public int? OperatedBy { get; set; }
        public int? OperatedByAdmin { get; set; }
    }
}