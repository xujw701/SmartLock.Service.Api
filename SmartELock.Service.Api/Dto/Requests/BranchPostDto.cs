using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartELock.Service.Api.Dto.Requests
{
    public class BranchPostDto
    {
        [Required]
        public int CompanyId { get; set; }

        [Required]
        public string BranchName { get; set; }

        public string Address { get; set; }
    }
}