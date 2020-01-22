using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartELock.Service.Api.Dto.Responses
{
    public class SuperAdminLoginResponseDto
    {
        public int SuperAdminId { get; set; }

        public string Token { get; set; }
    }
}