using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Models.Commands
{
    public class KeyboxPropertyUpdateCommand : IKeyboxPropertyCreateUpdateCommand
    {
        public int KeyboxId { get; set; }
        public int PropertyId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public string KeyboxName { get; set; }
        public string PropertyName { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public string Price { get; set; }
        public double? Bedrooms { get; set; }
        public double? Bathrooms { get; set; }
        public double? FloorArea { get; set; }
        public double? LandArea { get; set; }

        public int? OperatedBy { get; set; }
        public int? OperatedByAdmin { get; set; }
    }
}
