using CodeYad_Blog.DataLayer;
using Microsoft.AspNetCore.Http;

namespace CodeYad_Blog.CoreLayer.DTOs.Categories
{
    public class CreateCategoryDto
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public SeoData SeoData { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}