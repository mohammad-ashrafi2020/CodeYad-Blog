using CodeYad_Blog.DataLayer.Entities;
using Microsoft.AspNetCore.Http;

namespace CodeYad_Blog.CoreLayer.DTOs.Users
{
    public class EditUserDto
    {
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string? Email { get; set; }
        public string? AboutMe { get; set; }
        public IFormFile? Avatar { get; set; }
        public UserRole Role { get; set; }

    }
}