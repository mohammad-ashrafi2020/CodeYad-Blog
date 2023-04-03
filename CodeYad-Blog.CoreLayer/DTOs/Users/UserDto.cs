using System;
using CodeYad_Blog.DataLayer.Entities;

namespace CodeYad_Blog.CoreLayer.DTOs.Users
{
    public class UserDto
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}