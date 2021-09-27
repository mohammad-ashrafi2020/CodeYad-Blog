using System.Collections.Generic;
using CodeYad_Blog.CoreLayer.Utilities;

namespace CodeYad_Blog.CoreLayer.DTOs.Users
{
    public class UserFilterDto:BasePagination
    {
        public List<UserDto> Users { get; set; }
    }

}