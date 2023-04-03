using System.Collections.Generic;
using CodeYad_Blog.CoreLayer.DTOs.Categories;
using CodeYad_Blog.CoreLayer.Utilities;

namespace CodeYad_Blog.CoreLayer.Services.Categories
{
    public interface ICategoryService
    {
        Task<OperationResult> CreateCategory(CreateCategoryDto command);
        Task<OperationResult> EditCategory(EditCategoryDto command);
        Task<List<CategoryDto>> GetAllCategory();
        Task<CategoryDto?> GetCategoryBy(long id);
        Task<CategoryDto?> GetCategoryBy(string slug);
        Task<bool> IsSlugExist(string slug);
    }
}