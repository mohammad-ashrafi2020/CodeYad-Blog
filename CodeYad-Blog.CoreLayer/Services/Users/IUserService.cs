using CodeYad_Blog.CoreLayer.DTOs.Users;
using CodeYad_Blog.CoreLayer.Utilities;
using CodeYad_Blog.DataLayer.Entities;

namespace CodeYad_Blog.CoreLayer.Services.Users
{
    public interface IUserService
    {
        OperationResult EditUser(EditUserDto command);
        OperationResult RegisterUser(UserRegisterDto registerDto);
        UserDto LoginUser(LoginUserDto loginDto);
        UserDto GetUserById(int userId);
        UserFilterDto GetUsersByFilter(int pageId, int take);
    }
}