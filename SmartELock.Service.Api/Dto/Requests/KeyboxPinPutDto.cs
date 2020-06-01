using System.ComponentModel.DataAnnotations;

namespace SmartELock.Service.Api.Dto.Requests
{
    public class KeyboxPinPutDto
    {
        [Required]
        public string Pin { get; set; }
    }
}