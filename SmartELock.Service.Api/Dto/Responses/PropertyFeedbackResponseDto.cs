using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartELock.Service.Api.Dto.Responses
{
    public class PropertyFeedbackResponseDto
    {
        public int PropertyFeedbackId { get; set; }
        public int PropertyId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int? ResPortraitId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}