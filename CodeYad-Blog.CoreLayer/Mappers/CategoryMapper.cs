using CodeYad_Blog.CoreLayer.DTOs.Categories;
using CodeYad_Blog.DataLayer.Entities;

namespace CodeYad_Blog.CoreLayer.Mappers
{
    public class CategoryMapper
    {
        public static CategoryDto Map(Category category)
        {
            return new CategoryDto()
            {
                Slug = category.Slug,
                Id = category.Id,
                Title = category.Title,
                ImageName = category.ImageName,
                SeoData = category.SeoData
            };
        }
    }
}