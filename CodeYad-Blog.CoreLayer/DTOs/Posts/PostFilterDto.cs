using System.Collections.Generic;
using CodeYad_Blog.CoreLayer.Utilities;

namespace CodeYad_Blog.CoreLayer.DTOs.Posts
{
    public class PostFilterDto:BasePagination
    {
        public List<PostDto> Posts { get; set; }
        public PostFilterParams FilterParams { get; set; }
    }

    public class PostFilterParams
    {
        public string Search { get; set; }
        public string CategorySlug { get; set; }
        public string Tag { get; set; }
        public int PageId { get; set; }
        public int Take { get; set; }
    }
}