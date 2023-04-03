using CodeYad_Blog.CoreLayer.DTOs.Users;
using CodeYad_Blog.CoreLayer.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Infrastructure;
using OperationResult = CodeYad_Blog.CoreLayer.Utilities.OperationResult;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("Current")]
        public async Task<ApiResult<UserDto>> GetCurrent()
        {
            return QueryResult(await _userService.GetUserById(User.GetUserId()));
        }

        [HttpPut("Current")]
        public async Task<ApiResult> EditProfile([FromForm]EditUserDto command)
        {
            command.UserId = User.GetUserId();
            var result = await _userService.EditUser(command);

            return CommandResult(result);
        }

        [HttpPost("Current/Follow")]
        public async Task<ApiResult> Follow(string targetUserName)
        {
            var user = await _userService.GetUserByUserName(targetUserName);
            if (user == null)
                return CommandResult(OperationResult.NotFound());

            var result = await _userService.FollowUser(User.GetUserId(), user.UserId);

            return CommandResult(result);
        }

        [HttpPost("Current/UnFollow")]
        public async Task<ApiResult> UnFollow(string targetUserName)
        {
            var user = await _userService.GetUserByUserName(targetUserName);
            if (user == null)
                return CommandResult(OperationResult.NotFound());

            var result = await _userService.UnFollowUser(User.GetUserId(), user.UserId);

            return CommandResult(result);
        }
    }
}
