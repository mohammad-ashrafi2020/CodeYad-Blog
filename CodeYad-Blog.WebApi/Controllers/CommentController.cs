using CodeYad_Blog.CoreLayer.DTOs.Comments;
using CodeYad_Blog.CoreLayer.DTOs.Users;
using CodeYad_Blog.CoreLayer.Services.Comments;
using CodeYad_Blog.CoreLayer.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeYad_Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _service;

        public CommentController(ICommentService comment)
        {
            _service = comment;
        }
        [HttpGet]
        public List<CommentDto> GetList(int postId)
        {
            return _service.GetPostComments(postId);
        }

        [HttpPost]
        public IActionResult Create(CreateCommentDto comment)
        {
            var result = _service.CreateComment(comment);
            if (result.Status != OperationResultStatus.Success)
                return BadRequest(result.Message);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.DeleteComment(id);
            if (result.Status != OperationResultStatus.Success)
                return BadRequest(result.Message);

            return Ok();
        }
    }
}
