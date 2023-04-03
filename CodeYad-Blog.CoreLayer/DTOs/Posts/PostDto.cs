using System;
using CodeYad_Blog.CoreLayer.DTOs.Categories;
using CodeYad_Blog.CoreLayer.DTOs.Users;

namespace CodeYad_Blog.CoreLayer.DTOs.Posts
{
    public class PostDto
    {
        public long PostId { get; set; }
        public long CategoryId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public int Visit { get; set; }
        public bool IsSpecial { get; set; }

        public DateTime CreationDate { get; set; }
        public CategoryDto Category { get; set; }
        public UserDto Writer { get; set; }
    }
}