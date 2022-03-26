using System;
using System.Collections.Generic;
using System.Linq;
using CodeYad_Blog.CoreLayer.DTOs.Categories;
using CodeYad_Blog.CoreLayer.Mappers;
using CodeYad_Blog.CoreLayer.Utilities;
using CodeYad_Blog.DataLayer.Context;
using CodeYad_Blog.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace CodeYad_Blog.CoreLayer.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly BlogContext _context;

        public CategoryService(BlogContext context)
        {
            _context = context;
        }

        public OperationResult CreateCategory(CreateCategoryDto command)
        {
            if (IsSlugExist(command.Slug))
                return OperationResult.Error("Slug Is Exist");

            var category = new Category()
            {
                Title = command.Title,
                IsDelete = false,
                ParentId = command.ParentId,
                Slug = command.Slug.ToSlug(),
                MetaTag = command.MetaTag,
                MetaDescription = command.MetaDescription
            };
            _context.Categories.Add(category);
            _context.SaveChanges();
            return OperationResult.Success();
        }

        public OperationResult EditCategory(EditCategoryDto command)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == command.Id);
            if (category == null)
                return OperationResult.NotFound();

            if (command.Slug.ToSlug() != category.Slug)
                if (IsSlugExist(command.Slug))
                    return OperationResult.Error("Slug Is Exist");

            category.MetaDescription = command.MetaDescription;
            category.Slug = command.Slug.ToSlug();
            category.Title = command.Title;
            category.MetaTag = command.MetaTag;

            //_context.Update(category);
            _context.SaveChanges();
            return OperationResult.Success();
        }

        public List<CategoryDto> GetAllCategory()
        {
            return _context.Categories.Select(category => CategoryMapper.Map(category)).ToList();
        }

        public CategoryDto GetCategoryBy(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
                return null;

            return CategoryMapper.Map(category);
        }

        public CategoryDto GetCategoryBy(string slug)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Slug == slug);
            if (category == null)
                return null;
            return CategoryMapper.Map(category);
        }

        public OperationResult DeleteCategory(int id)
        {
            var category = _context.Categories
                .Include(c => c.SubPosts)
                .Include(c => c.Posts)
                .FirstOrDefault(f=>f.Id==id);
            if(category==null)
                return OperationResult.NotFound("اطلاعات یافت نشد");

            if(category.Posts.Any() || category.SubPosts.Any())
                return OperationResult.Error("امکان حذف این دسته بندی وجود ندارد");

            try
            {
                var childCategory = _context.Categories
                    .Include(c => c.SubPosts)
                    .Include(c => c.Posts)
                    .Where(f => f.ParentId == category.Id).ToList();
                foreach (var child in childCategory)
                {
                    if (child.Posts.Any() || child.SubPosts.Any())
                        return OperationResult.Error("امکان حذف این دسته بندی وجود ندارد");
                }
                _context.Remove(category);
                _context.RemoveRange(childCategory);
                _context.SaveChanges();
                return OperationResult.Success();
            }
            catch (Exception e)
            {
               return OperationResult.Error(e.Message);
            }
        }

        public List<CategoryDto> GetChildCategories(int parentId)
        {
            return _context.Categories.Where(r=>r.ParentId==parentId)
                .Select(category => CategoryMapper.Map(category)).ToList();
        }

        public bool IsSlugExist(string slug)
        {
            return _context.Categories.Any(c => c.Slug == slug.ToSlug());
        }

       
    }
}