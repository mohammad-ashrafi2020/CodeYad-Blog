using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CodeYad_Blog.CoreLayer.DTOs.Comments;
using CodeYad_Blog.CoreLayer.DTOs.Posts;
using CodeYad_Blog.CoreLayer.Services.Comments;
using CodeYad_Blog.CoreLayer.Services.Posts;
using CodeYad_Blog.CoreLayer.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeYad_Blog.Web.Pages
{
    [ValidateAntiForgeryToken]
    public class PostModel : PageModel
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        public PostModel(IPostService postService, ICommentService commentService)
        {
            _postService = postService;
            _commentService = commentService;
        }

        public PostDto Post { get; set; }

        [Required]
        [BindProperty]
        public string Text { get; set; }
        [BindProperty]
        public int PostId { get; set; }


        public List<CommentDto> Comments { get; set; }
        public List<PostDto> RelatedPosts { get; set; }
        public IActionResult OnGet(string slug)
        {
            Post = _postService.GetPostBySlug(slug);
            if (Post == null)
                return NotFound();

            Comments = _commentService.GetPostComments(Post.PostId);
            RelatedPosts = _postService.GetRelatedPosts(Post.SubCategoryId ?? Post.CategoryId);
            _postService.IncreaseVisit(Post.PostId);
            return Page();
        }

        public IActionResult OnPost(string slug)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("Post", new { slug });

            if (!ModelState.IsValid)
            {
                Post = _postService.GetPostBySlug(slug);
                Comments = _commentService.GetPostComments(Post.PostId);
                RelatedPosts = _postService.GetRelatedPosts(Post.SubCategoryId ?? Post.CategoryId);
                return Page();
            }

            _commentService.CreateComment(new CreateCommentDto()
            {
                PostId = PostId,
                Text = Text,
                UserId = User.GetUserId()
            });

            return RedirectToPage("Post", new { slug });
        }
    }
}
