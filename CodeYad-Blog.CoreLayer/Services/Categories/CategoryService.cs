using System;
using System.Collections.Generic;
using System.Linq;
using CodeYad_Blog.CoreLayer.DTOs.Categories;
using CodeYad_Blog.CoreLayer.Mappers;
using CodeYad_Blog.CoreLayer.Services.FileManager;
using CodeYad_Blog.CoreLayer.Utilities;
using CodeYad_Blog.DataLayer.Context;
using CodeYad_Blog.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeYad_Blog.CoreLayer.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly BlogContext _context;
        private readonly IFileManager _fileManager;

        public CategoryService(BlogContext context, IFileManager fileManager)
        {
            _context = context;
            _fileManager = fileManager;
        }

        public async Task<OperationResult> CreateCategory(CreateCategoryDto command)
        {
            if (await IsSlugExist(command.Slug))
                return OperationResult.Error("Slug Is Exist");

            var category = new Category()
            {
                Title = command.Title,
                Slug = command.Slug.ToSlug(),
                SeoData = command.SeoData,
            };

            if (ImageValidation.IsImage(command.ImageFile))
                category.ImageName =
                    await _fileManager.SaveFileAndReturnNameAsync(command.ImageFile, Directories.Categories);

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return OperationResult.Success();
        }

        public async Task<OperationResult> EditCategory(EditCategoryDto command)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == command.Id);
            if (category == null)
                return OperationResult.NotFound();

            if (command.Slug.ToSlug() != category.Slug)
                if (await IsSlugExist(command.Slug))
                    return OperationResult.Error("Slug Is Exist");

            if (command.ImageFile != null && ImageValidation.IsImage(command.ImageFile))
                category.ImageName =
                    await _fileManager.SaveFileAndReturnNameAsync(command.ImageFile, Directories.Categories);


            category.Slug = command.Slug.ToSlug();
            category.Title = command.Title;
            category.SeoData = command.SeoData;

            _context.Update(category);
            await _context.SaveChangesAsync();
            return OperationResult.Success();
        }

        public async Task<List<CategoryDto>> GetAllCategory()
        {
            return await _context.Categories.Select(category => CategoryMapper.Map(category)).ToListAsync();
        }

        public async Task<CategoryDto?> GetCategoryBy(long id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
                return null;

            return CategoryMapper.Map(category);
        }

        public async Task<CategoryDto?> GetCategoryBy(string slug)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Slug == slug);
            if (category == null)
                return null;
            return CategoryMapper.Map(category);
        }

        public async Task<bool> IsSlugExist(string slug)
        {
            return await _context.Categories.AnyAsync(c => c.Slug == slug.ToSlug());
        }
    }
}