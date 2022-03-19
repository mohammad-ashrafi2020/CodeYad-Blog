using CodeYad_Blog.CoreLayer.DTOs.Users;
using CodeYad_Blog.CoreLayer.Services.Users;
using CodeYad_Blog.CoreLayer.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeYad_Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService userService)
        {
            _service = userService;
        }

        [HttpGet]
        public UserFilterDto GetList(int pageId, int take)
        {
            return _service.GetUsersByFilter(pageId, take);
        }

        [HttpGet("{id}")]
        public UserDto GetById(int id)
        {
            return _service.GetUserById(id);
        }

        [HttpPost]
        public IActionResult Create(UserRegisterDto user)
        {
            var result = _service.RegisterUser(user);
            if (result.Status != OperationResultStatus.Success)
                return BadRequest(result.Message);

            return Ok();
        }
        [HttpPut]
        public IActionResult Edit(EditUserDto user)
        {
            var result = _service.EditUser(user);
            if (result.Status != OperationResultStatus.Success)
                return BadRequest(result.Message);

            return Ok();
        }
    }
}
