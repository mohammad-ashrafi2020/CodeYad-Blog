using System.Collections.Generic;
using System.Linq;
using CodeYad_Blog.CoreLayer.DTOs.Posts;
using CodeYad_Blog.CoreLayer.Mappers;
using CodeYad_Blog.CoreLayer.Services.FileManager;
using CodeYad_Blog.CoreLayer.Utilities;
using CodeYad_Blog.DataLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace CodeYad_Blog.CoreLayer.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly BlogContext _context;
        private readonly IFileManager _fileManger;
        public PostService(BlogContext context, IFileManager fileManager)
        {
            _context = context;
            _fileManger = fileManager;
        }

        public OperationResult CreatePost(CreatePostDto command)
        {
            if (command.ImageFile == null)
                return OperationResult.Error();
            var post = PostMapper.MapCreateDtoToPost(command);

            if (IsSlugExist(post.Slug))
                return OperationResult.Error("Slug تکراری است");

            post.ImageName = _fileManger.SaveFileAndReturnName(command.ImageFile, Directories.PostImage);
            _context.Posts.Add(post);
            _context.SaveChanges();

            return OperationResult.Success();
        }

        public OperationResult EditPost(EditPostDto command)
        {
            var post = _context.Posts.FirstOrDefault(c => c.Id == command.PostId);
            if (post == null)
                return OperationResult.NotFound();

            var oldImage = post.ImageName;
            var newSlug = command.Slug.ToSlug();

            if (post.Slug != newSlug)
                if (IsSlugExist(newSlug))
                    return OperationResult.Error("Slug تکراری است");

            PostMapper.EditPost(command, post);
            if (command.ImageFile != null)
                post.ImageName = _fileManger.SaveFileAndReturnName(command.ImageFile, Directories.PostImage);

            _context.SaveChanges();

            if (command.ImageFile != null)
                _fileManger.DeleteFile(oldImage, Directories.PostImage);

            return OperationResult.Success();
        }

        public PostDto GetPostById(int postId)
        {
            var post = _context.Posts
                .Include(c => c.SubCategory)
                .Include(c => c.Category)
                .FirstOrDefault(c => c.Id == postId);
            return PostMapper.MapToDto(post);
        }

        public PostDto GetPostBySlug(string slug)
        {
            var post = _context.Posts
                .Include(c => c.SubCategory)
                .Include(c => c.Category)
                .Include(c => c.User)
                .FirstOrDefault(c => c.Slug == slug);
            if (post == null)
                return null;

            return PostMapper.MapToDto(post);
        }

        public PostFilterDto GetPostsByFilter(PostFilterParams filterParams)
        {
            var result = _context.Posts
                .Include(d => d.Category)
                .Include(d => d.SubCategory)
                .OrderByDescending(d => d.CreationDate)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterParams.CategorySlug))
                result = result.Where(r => r.Category.Slug == filterParams.CategorySlug);

            if (!string.IsNullOrWhiteSpace(filterParams.Title))
                result = result.Where(r => r.Title.Contains(filterParams.Title));

            var skip = (filterParams.PageId - 1) * filterParams.Take;
            var pageCount = result.Count() / filterParams.Take;

            return new PostFilterDto()
            {
                Posts = result.Skip(skip).Take(filterParams.Take)
                    .Select(post => PostMapper.MapToDto(post)).ToList(),
                FilterParams = filterParams,
                PageCount = pageCount
            };
        }

        public List<PostDto> GetRelatedPosts(int categoryId)
        {
            return _context.Posts
                .Where(r => r.CategoryId == categoryId || r.SubCategoryId == categoryId)
                .OrderByDescending(d => d.CreationDate)
                .Take(6).Select(post => PostMapper.MapToDto(post)).ToList();
        }

        public List<PostDto> GetPopularPost()
        {
            return _context.Posts
                .Include(c=>c.User)
                .OrderByDescending(d => d.Visit)
                .Take(6).Select(post => PostMapper.MapToDto(post)).ToList();
        }

        public void IncreaseVisit(int postId)
        {
            var post = _context.Posts.First(p => p.Id == postId);
            post.Visit += 1;
            _context.SaveChanges();
        }

        public bool IsSlugExist(string slug)
        {
            return _context.Posts.Any(p => p.Slug == slug.ToSlug());
        }
    }
}