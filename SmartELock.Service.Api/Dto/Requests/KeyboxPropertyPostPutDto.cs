using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartELock.Service.Api.Dto.Requests
{
    public class KeyboxPropertyPostPutDto
    {
        [Required]
        public int CompanyId { get; set; }

        [Required]
        public int BranchId { get; set; }

        [Required]
        public string KeyboxName { get; set; }

        [Required]
        public string PropertyName { get; set; }

        [Required]
        public string Address { get; set; }

        public string Notes { get; set; }

        public string Price { get; set; }

        public double? Bedrooms { get; set; }

        public double? Bathrooms { get; set; }

        public double? FloorArea { get; set; }

        public double? LandArea { get; set; }
    }
}