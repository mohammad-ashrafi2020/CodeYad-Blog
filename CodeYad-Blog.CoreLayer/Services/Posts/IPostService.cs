using System.Collections.Generic;
using CodeYad_Blog.CoreLayer.DTOs.Posts;
using CodeYad_Blog.CoreLayer.Utilities;

namespace CodeYad_Blog.CoreLayer.Services.Posts
{
    public interface IPostService
    {
        Task<OperationResult> CreatePost(CreatePostDto command);
        Task<OperationResult> EditPost(EditPostDto command);
        PostDto? GetPostById(long postId);
        Task<PostDto?> GetPostBySlug(string slug);
        Task<PostFilterDto> GetPostsByFilter(PostFilterParams filterParams);
        bool IsSlugExist(string slug);
        List<PostDto> GetRelatedPosts(long categoryId);
        List<PostDto> GetPopularPost();
        Task IncreaseVisit(long postId);
    }
}