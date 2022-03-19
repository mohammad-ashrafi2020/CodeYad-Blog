using CodeYad_Blog.CoreLayer.DTOs.Posts;
using CodeYad_Blog.CoreLayer.DTOs.Users;
using CodeYad_Blog.CoreLayer.Services.Posts;
using CodeYad_Blog.CoreLayer.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeYad_Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _service;

        public PostController(IPostService service)
        {
            _service = service;
        }

        [HttpGet("popular")]
        public List<PostDto> GetList()
        {
            return _service.GetPopularPost();
        }
        [HttpGet]
        public PostFilterDto GetList(PostFilterParams @params)
        {
            return _service.GetPostsByFilter(@params);
        }

        [HttpGet("{id}")]
        public PostDto GetById(int id)
        {
            _service.IncreaseVisit(id);
            return _service.GetPostById(id);
        }

        [HttpPost]
        public IActionResult Create(CreatePostDto post)
        {
            var result = _service.CreatePost(post);
            if (result.Status != OperationResultStatus.Success)
                return BadRequest(result.Message);

            return Ok();
        }
        [HttpPut]
        public IActionResult Edit(EditPostDto post)
        {
            var result = _service.EditPost(post);
            if (result.Status != OperationResultStatus.Success)
                return BadRequest(result.Message);

            return Ok();
        }
    }
}
