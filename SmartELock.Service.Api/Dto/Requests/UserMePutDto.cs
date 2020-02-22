using System.ComponentModel.DataAnnotations;

namespace SmartELock.Service.Api.Dto.Requests
{
    public class UserMePutDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        public string Password { get; set; }
    }
}