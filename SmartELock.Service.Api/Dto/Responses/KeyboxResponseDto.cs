using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartELock.Service.Api.Dto.Responses
{
    public class KeyboxResponseDto
    {
        public int KeyboxId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public string Uuid { get; set; }
        public int? PropertyId { get; set; }
        public string KeyboxName { get; set; }
        public int BatteryLevel { get; set; }
    }
}