using System.Collections.Generic;
using CodeYad_Blog.CoreLayer.DTOs.Categories;
using CodeYad_Blog.CoreLayer.DTOs.Posts;

namespace CodeYad_Blog.CoreLayer.DTOs
{
    public class MainPageDto
    {
        public List<PostDto> LatestPosts { get; set; }
        public List<PostDto> SpecialPosts { get; set; }
        public List<MainPageCategoryDto> Categories { get; set; }
    }

    public class MainPageCategoryDto
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public int PostChild { get; set; }
    }
}