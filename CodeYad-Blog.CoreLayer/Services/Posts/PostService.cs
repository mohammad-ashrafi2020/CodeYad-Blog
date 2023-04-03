using System.Collections.Generic;
using System.Linq;
using CodeYad_Blog.CoreLayer.DTOs.Posts;
using CodeYad_Blog.CoreLayer.Mappers;
using CodeYad_Blog.CoreLayer.Services.FileManager;
using CodeYad_Blog.CoreLayer.Utilities;
using CodeYad_Blog.DataLayer.Context;
using CodeYad_Blog.DataLayer.Entities;
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

        public async Task<OperationResult> CreatePost(CreatePostDto command)
        {
            if (command.ImageFile == null)
                return OperationResult.Error();
            var post = PostMapper.MapCreateDtoToPost(command);

            if (IsSlugExist(post.Slug))
                return OperationResult.Error("Slug تکراری است");

            post.ImageName = _fileManger.SaveImageAndReturnImageName(command.ImageFile, Directories.PostImage);
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            await SyncPostTags(post.Tags, post.Id, post.UserId);
            return OperationResult.Success();
        }

        public async Task<OperationResult> EditPost(EditPostDto command)
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
                post.ImageName = _fileManger.SaveImageAndReturnImageName(command.ImageFile, Directories.PostImage);

            await _context.SaveChangesAsync();
            await SyncPostTags(post.Tags, post.Id, post.UserId);

            if (command.ImageFile != null)
                _fileManger.DeleteFile(oldImage, Directories.PostImage);

            return OperationResult.Success();
        }

        public PostDto? GetPostById(long postId)
        {
            var post = _context.Posts
                .Include(c => c.Category)
                .FirstOrDefault(c => c.Id == postId);

            if (post == null)
                return null;

            return PostMapper.MapToDto(post);
        }

        public async Task<PostDto?> GetPostBySlug(string slug)
        {
            var post = await _context.Posts
                .Include(c => c.Category)
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Slug == slug);

            if (post == null)
                return null;

            return PostMapper.MapToDto(post);
        }

        public async Task<PostFilterDto> GetPostsByFilter(PostFilterParams filterParams)
        {
            var result = _context.Posts
                .Include(d => d.Category)
                .OrderByDescending(d => d.CreationDate)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterParams.CategorySlug))
                result = result.Where(r => r.Category.Slug == filterParams.CategorySlug);

            if (!string.IsNullOrWhiteSpace(filterParams.Tag))
                result = result.Where(r => r.Tags == filterParams.Tag);

            if (!string.IsNullOrWhiteSpace(filterParams.Search))
                result = result.Where(r => r.Title.Contains(filterParams.Search) ||
                                           r.Tags.Contains(filterParams.Search) ||
                                           r.Category.Title.Contains(filterParams.Search));

            var skip = (filterParams.PageId - 1) * filterParams.Take;
            var model = new PostFilterDto()
            {
                Posts = await result.Skip(skip).Take(filterParams.Take)
                    .Select(post => PostMapper.MapToDto(post)).ToListAsync(),
                FilterParams = filterParams,
            };
            model.GeneratePaging(result, filterParams.Take, filterParams.PageId);

            return model;
        }

        public List<PostDto> GetRelatedPosts(long categoryId)
        {
            return _context.Posts
                .Where(r => r.CategoryId == categoryId)
                .OrderByDescending(d => d.CreationDate)
                .Take(6).Select(post => PostMapper.MapToDto(post)).ToList();
        }

        public List<PostDto> GetPopularPost()
        {
            return _context.Posts
                .Include(c => c.User)
                .OrderByDescending(d => d.Visit)
                .Take(6).Select(post => PostMapper.MapToDto(post)).ToList();
        }

        public async Task IncreaseVisit(long postId)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            if (post == null)
            {
                return;
            }
            post.Visit += 1;
            await _context.SaveChangesAsync();
        }

        public bool IsSlugExist(string slug)
        {
            return _context.Posts.Any(p => p.Slug == slug.ToSlug());
        }

        private async Task SyncPostTags(string tags, long postId, long userId)
        {
            var newTags = new List<Tag>();
            var allTags = new List<Tag>();

            foreach (var tag in tags.Split("-"))
            {
                var currentTag = await _context.Tags.FirstOrDefaultAsync(f => f.Title == tag.ToLower());
                if (currentTag == null)
                {
                    newTags.Add(new Tag()
                    {
                        Title = tag,
                        UserCreatorId = userId,
                        CreationDate = DateTime.Now
                    });
                }
                else
                {
                    allTags.Add(new Tag()
                    {
                        Title = tag,
                        UserCreatorId = userId,
                        CreationDate = DateTime.Now
                    });
                }
            }
            if (newTags.Any())
            {
                _context.Tags.AddRange(newTags);
                await _context.SaveChangesAsync();
            }
            allTags.AddRange(newTags);

            allTags.ForEach(f =>
            {
                _context.PostTags.Add(new PostTag()
                {
                    PostId = postId,
                    TagId = f.Id,
                    CreationDate = DateTime.Now
                });

            });
            var oldPostTags = await _context.PostTags.Where(r => r.PostId == postId).ToListAsync();
            if (oldPostTags.Any())
            {
                _context.PostTags.RemoveRange(oldPostTags);
            }
            await _context.SaveChangesAsync();
        }
    }
}