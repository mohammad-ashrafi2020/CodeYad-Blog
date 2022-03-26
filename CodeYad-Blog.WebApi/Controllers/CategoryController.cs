using CodeYad_Blog.CoreLayer.DTOs.Categories;
using CodeYad_Blog.CoreLayer.DTOs.Users;
using CodeYad_Blog.CoreLayer.Services.Categories;
using CodeYad_Blog.CoreLayer.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeYad_Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }
        [HttpGet]
        public List<CategoryDto> GetList()
        {
            return _service.GetAllCategory();
        }
        [HttpGet("GetChild/{parentId}")]
        public List<CategoryDto> GetList(int parentId)
        {
            return _service.GetChildCategories(parentId);
        }
        [HttpGet("{id}")]
        public CategoryDto GetById(int id)
        {
            return _service.GetCategoryBy(id);
        }
        [HttpGet("GetBySlug/{slug}")]
        public CategoryDto GetBySlug(string slug)
        {
            return _service.GetCategoryBy(slug);
        }
        [HttpPost]
        public IActionResult Create(CreateCategoryDto category)
        {
            var result = _service.CreateCategory(category);
            if (result.Status != OperationResultStatus.Success)
                return BadRequest(result.Message);

            return Ok();
        }

        [HttpPut]
        public IActionResult Edit(EditCategoryDto category)
        {
            var result = _service.EditCategory(category);
            if (result.Status != OperationResultStatus.Success)
                return BadRequest(result.Message);

            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.DeleteCategory(id);
            if (result.Status != OperationResultStatus.Success)
                return BadRequest(result.Message);

            return Ok();
        }
    }
}
