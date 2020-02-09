﻿using System;

namespace SmartELock.Core.Domain.Models.Snapshots
{
    public class PropertySnapshot
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
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
