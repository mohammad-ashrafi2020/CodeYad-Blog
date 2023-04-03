using CodeYad_Blog.CoreLayer.DTOs.Users;
using CodeYad_Blog.CoreLayer.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Infrastructure;
using WebApi.Infrastructure.JwtUtil;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ApiController
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;
    public AuthController(IUserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }

    [HttpPost("Register")]
    public async Task<ApiResult> Register([FromBody] UserRegisterDto command)
    {
        return CommandResult(await _userService.RegisterUser(command));
    }

    [HttpPost("Login")]
    public async Task<ApiResult<LoginResult>> Login([FromBody] LoginUserDto command)
    {
        var user = await _userService.LoginUser(command);
        if (user == null)
        {
            return QueryResult(default(LoginResult));
        }

        return QueryResult(new LoginResult()
        {
            UserDto = user,
            Token = JwtTokenBuilder.BuildToken(user, _configuration)
        });
    }
}

public class LoginResult
{
    public UserDto UserDto { get; set; }
    public string Token { get; set; }
}