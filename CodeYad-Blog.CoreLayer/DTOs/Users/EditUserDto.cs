using CodeYad_Blog.DataLayer.Entities;

namespace CodeYad_Blog.CoreLayer.DTOs.Users
{
    public class EditUserDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public UserRole Role { get; set; }

    }
}