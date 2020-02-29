using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Models.Snapshots
{
    public class ResPropertySnapshot
    {
        public int ResPropertyId { get; set; }
        public int PropertyId { get; set; }
        public string Url { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
