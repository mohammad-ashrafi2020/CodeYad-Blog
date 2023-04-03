using CodeYad_Blog.CoreLayer.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebApi.Infrastructure.JwtUtil;

public class CustomJwtValidation
{
    private readonly IUserService _userFacade;

    public CustomJwtValidation(IUserService facade)
    {
        _userFacade = facade;
    }

    public async Task Validate(TokenValidatedContext context)
    {
        var userId = context.Principal.GetUserId();
        var jwtToken = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        //var token = await _userFacade.GetUserTokenByJwtToken(jwtToken);
        //if (token == null)
        //{
        //    context.Fail("Token NotFound");
        //}
    }
}