using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartELock.Service.Api.Dto.Responses
{
    public class KeyboxHistoryResponseDto
    {
        public int KeyboxHistoryId { get; set; }
        public int KeyboxId { get; set; }
        public int UserId { get; set; }
        public int? TmpUserId { get; set; }
        public int PropertyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime InOn { get; set; }
        public DateTime? OutOn { get; set; }
    }
}