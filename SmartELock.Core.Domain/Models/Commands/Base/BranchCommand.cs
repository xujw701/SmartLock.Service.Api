﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Models.Commands.Base
{
    public class BranchCommand : IBranchCommand
    {
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public int? OperatedBy { get; set; }

        public int? OperatedByAdmin { get; set; }
    }
}
