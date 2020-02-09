using System.ComponentModel.DataAnnotations;

namespace SmartELock.Service.Api.Dto.Requests
{
    public class PropertyPostDto
    {
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