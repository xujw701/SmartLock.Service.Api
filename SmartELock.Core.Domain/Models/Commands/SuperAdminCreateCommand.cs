using SmartELock.Core.Domain.Models.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Models.Commands
{
    public class SuperAdminCreateCommand : SuperAdminCommand
    {
        public string Password { get; set; }
    }
}
