using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Models.Commands
{
    public class SuperAdminCreateCommand : ISuperAdminCommand
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
