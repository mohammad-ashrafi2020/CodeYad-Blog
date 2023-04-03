using Microsoft.AspNetCore.Http;

namespace CodeYad_Blog.CoreLayer.DTOs.Posts
{
    public class CreatePostDto
    {
        public long UserId { get; set; }
        public long CategoryId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public bool IsSpecial { get; set; }
        public string Tags { get; set; }
        public string ShortDescription { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}