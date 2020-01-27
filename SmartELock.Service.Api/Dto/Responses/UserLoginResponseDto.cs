using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartELock.Service.Api.Dto.Responses
{
    public class UserLoginResponseDto
    {
        public int UserId { get; set; }

        public string Token { get; set; }
    }
}