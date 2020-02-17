using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartELock.Service.Api.Dto.Responses
{
    public class PropertyResponseDto
    {
        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public string Price { get; set; }
        public double? Bedrooms { get; set; }
        public double? Bathrooms { get; set; }
        public double? FloorArea { get; set; }
        public double? LandArea { get; set; }
    }
}