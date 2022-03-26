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
        public PostFilterDto GetList([FromQuery] PostFilterParams @params)
        {
            return _service.GetPostsByFilter(@params);
        }

        [HttpGet("{id}")]
        public PostDto GetById(int id)
        {
            return _service.GetPostById(id);
        }
        [HttpGet("getBySlug/{slug}")]
        public PostDto GetBySlug(string slug)
        {
            var post = _service.GetPostBySlug(slug);
            if (post == null)
                return null;

            _service.IncreaseVisit(post.PostId);
            return post;
        }

        [HttpPost]
        public IActionResult Create([FromForm] CreatePostDto post)
        {
            var result = _service.CreatePost(post);
            if (result.Status != OperationResultStatus.Success)
                return BadRequest(result.Message);

            return Ok();
        }
        [HttpPut]
        public IActionResult Edit([FromForm] EditPostDto post)
        {
            var result = _service.EditPost(post);
            if (result.Status != OperationResultStatus.Success)
                return BadRequest(result.Message);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.DeletePost(id);

            if (result.Status != OperationResultStatus.Success)
                return BadRequest(result.Message);

            return Ok();
        }
    }
}
