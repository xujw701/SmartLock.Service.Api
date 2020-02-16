using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Models.Commands.Base
{
    public class CompanyCommand : ICompanyCommand
    {
        public string CompanyName { get; set; }
    }
}
