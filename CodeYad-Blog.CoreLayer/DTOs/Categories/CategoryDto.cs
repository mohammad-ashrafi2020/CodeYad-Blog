using CodeYad_Blog.DataLayer;

namespace CodeYad_Blog.CoreLayer.DTOs.Categories
{
    public class CategoryDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public SeoData SeoData { get; set; }
        public string? ImageName { get; set; }
    }
}