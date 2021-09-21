using System.Collections.Generic;

namespace CodeYad_Blog.CoreLayer.DTOs.Users
{
    public class UserFilterDto
    {
        public int PageCount { get; set; }
        public int PageId { get; set; }
        public List<UserDto> Users { get; set; }
    }

}