using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartELock.Service.Api.Dto.Requests
{
    public class FeedbackPostDto
    {
        [Required]
        public string Content { get; set; }
    }
}