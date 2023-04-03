using System.ComponentModel.DataAnnotations;

namespace CodeYad_Blog.CoreLayer.DTOs.Users
{
    public class UserRegisterDto
    {
        public string? Fullname { get; set; }
        [Required]
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }
    }
}