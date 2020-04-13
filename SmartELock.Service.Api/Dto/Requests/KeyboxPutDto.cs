using System.ComponentModel.DataAnnotations;

namespace SmartELock.Service.Api.Dto.Requests
{
    public class KeyboxPutDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string KeyboxName { get; set; }
    }
}