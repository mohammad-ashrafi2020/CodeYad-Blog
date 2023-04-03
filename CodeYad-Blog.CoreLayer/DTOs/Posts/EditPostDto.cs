using Microsoft.AspNetCore.Http;

namespace CodeYad_Blog.CoreLayer.DTOs.Posts
{
    public class EditPostDto
    {
        public long PostId { get; set; }
        public long CategoryId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string Tags { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}