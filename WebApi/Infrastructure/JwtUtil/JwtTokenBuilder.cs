using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CodeYad_Blog.CoreLayer.DTOs.Users;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Infrastructure.JwtUtil;

public class JwtTokenBuilder
{
    public static string BuildToken(UserDto user, IConfiguration configuration)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name,user.UserName),
            new Claim(ClaimTypes.MobilePhone,user.PhoneNumber),
            new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
        };
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:SignInKey"]));
        var credential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["JwtConfig:Issuer"],
            audience: configuration["JwtConfig:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(30),
            signingCredentials: credential);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}