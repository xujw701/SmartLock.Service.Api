using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartELock.Service.Api.Dto.Requests
{
    public class KeyboxPostDto
    {
        [Required]
        public int CompanyId { get; set; }

        [Required]
        public int BranchId { get; set; }

        [Required]
        public string Uuid { get; set; }

        [Required]
        public string KeyboxName { get; set; }

        [Required]
        public int BatteryLevel { get; set; }

        [Required]
        public string Pin { get; set; }
    }
}