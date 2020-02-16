using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Models.Commands.Base
{
    public class SuperAdminCommand : ISuperAdminCommand
    {
        public string Username { get; set; }
    }
}
