using System.Collections.Generic;
using CodeYad_Blog.CoreLayer.DTOs.Posts;
using CodeYad_Blog.CoreLayer.Utilities;

namespace CodeYad_Blog.CoreLayer.Services.Posts
{
    public interface IPostService
    {
        OperationResult CreatePost(CreatePostDto command);
        OperationResult EditPost(EditPostDto command);
        PostDto GetPostById(int postId);
        PostDto GetPostBySlug(string slug);
        PostFilterDto GetPostsByFilter(PostFilterParams filterParams);
        bool IsSlugExist(string slug);
        List<PostDto> GetRelatedPosts(int groupId);
        List<PostDto> GetPopularPost();
        void IncreaseVisit(int postId);
    }
}