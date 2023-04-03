using CodeYad_Blog.CoreLayer.DTOs.Posts;
using CodeYad_Blog.CoreLayer.DTOs.Users;
using CodeYad_Blog.CoreLayer.Utilities;
using CodeYad_Blog.DataLayer;
using CodeYad_Blog.DataLayer.Entities;

namespace CodeYad_Blog.CoreLayer.Mappers
{
    public class PostMapper
    {
        public static Post MapCreateDtoToPost(CreatePostDto dto)
        {
            return new Post()
            {
                Description = dto.Description,
                CategoryId = dto.CategoryId,
                Slug = dto.Slug.ToSlug(),
                Title = dto.Title,
                UserId = dto.UserId,
                Visit = 0,
                IsSpecial = dto.IsSpecial,
                SeoData = new SeoData()
                {
                    MetaTitle = dto.Title,
                    Canonical = null,
                    MetaDescription = dto.ShortDescription,
                    MetaKeyWords = dto.Tags.Replace("-", ",")
                },
                Tags = dto.Tags.ToLower(),

            };
        }
        public static PostDto MapToDto(Post post)
        {
          
            return new PostDto()
            {
                Description = post.Description,
                CategoryId = post.CategoryId,
                Slug = post.Slug,
                Title = post.Title,
                Visit = post.Visit,
                CreationDate = post.CreationDate,
                Category = CategoryMapper.Map(post.Category),
                ImageName = post.ImageName,
                PostId = post.Id,
                IsSpecial = post.IsSpecial,
                Writer = new UserDto()
                {
                    FullName = post.User.FullName,
                    Password = "",
                    Role = post.User.Role,
                    UserName = post.User.UserName,
                    RegisterDate = post.User.CreationDate,
                    UserId = post.User.Id,
                    PhoneNumber = post.User.PhoneNumber,
                    Email = post.User.Email
                }
            };
        }
        public static Post EditPost(EditPostDto editDto, Post post)
        {
            post.Description = editDto.Description;
            post.Title = editDto.Title;
            post.CategoryId = editDto.CategoryId;
            post.Slug = editDto.Slug.ToSlug();
            post.SeoData = new SeoData()
            {
                MetaDescription = editDto.ShortDescription,
                MetaKeyWords = editDto.Tags.Replace("-", ","),
                Canonical = null,
                MetaTitle = editDto.Title
            };
            post.Tags = editDto.Tags;

            return post;
        }
    }
}