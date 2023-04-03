using CodeYad_Blog.CoreLayer.DTOs.Users;
using CodeYad_Blog.CoreLayer.Utilities;
using CodeYad_Blog.DataLayer.Entities;

namespace CodeYad_Blog.CoreLayer.Services.Users
{
    public interface IUserService
    {
        Task<OperationResult> EditUser(EditUserDto command);
        Task<OperationResult> RegisterUser(UserRegisterDto registerDto);
        Task<UserDto?> LoginUser(LoginUserDto loginDto);
        Task<UserDto?> GetUserById(long userId);
        Task<UserDto?> GetUserByUserName(string userName);
        Task<UserFilterDto> GetUsersByFilter(int pageId, int take);

        Task<OperationResult> FollowUser(long userId, long targetUserId);
        Task<OperationResult> UnFollowUser(long userId, long targetUserId);
    }
}