using CodeYad_Blog.CoreLayer.DTOs.Users;
using CodeYad_Blog.CoreLayer.Services.FileManager;
using CodeYad_Blog.CoreLayer.Utilities;
using CodeYad_Blog.CoreLayer.Utilities.Security;
using CodeYad_Blog.DataLayer.Context;
using CodeYad_Blog.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeYad_Blog.CoreLayer.Services.Users;

public class UserService : IUserService
{
    private readonly BlogContext _context;
    private readonly IFileManager _fileManager;
    public UserService(BlogContext context, IFileManager fileManager)
    {
        _context = context;
        _fileManager = fileManager;
    }

    public async Task<OperationResult> EditUser(EditUserDto command)
    {
        var user = await _context.Users.FirstOrDefaultAsync(c => c.Id == command.UserId);
        if (user == null)
            return OperationResult.NotFound();

        user.AboutMe = command.AboutMe?.SanitizeText();
        user.FullName = command.FullName;
        user.Role = command.Role;
        user.Email = command.Email;

        if (command.Avatar != null)
            if (ImageValidation.IsImage(command.Avatar))
                user.Avatar = await _fileManager.SaveFileAndReturnNameAsync(command.Avatar, Directories.UserAvatar);

        await _context.SaveChangesAsync();
        return OperationResult.Success();
    }

    public async Task<OperationResult> RegisterUser(UserRegisterDto registerDto)
    {
        if (registerDto.UserName.IsUniCode())
            return OperationResult.Error("نام کاربری باید انگلیسی باشد");

        var isUserNameExist =await _context.Users.AnyAsync(u => u.UserName == registerDto.UserName);
        var isPhoneExist =await _context.Users.AnyAsync(u => u.PhoneNumber == registerDto.PhoneNumber);

        if (isUserNameExist)
            return OperationResult.Error("نام کاربری تکراری است");

        if (isPhoneExist)
            return OperationResult.Error("شماره تلفن تکراری است");

        var passwordHash = registerDto.Password.EncodeToMd5();
        _context.Users.Add(new User()
        {
            FullName = registerDto.Fullname,
            UserName = registerDto.UserName,
            PhoneNumber = registerDto.PhoneNumber,
            CreationDate = DateTime.Now,
            Password = passwordHash,
            Role = UserRole.User,
            Email = null
        });
        await _context.SaveChangesAsync();
        return OperationResult.Success();
    }

    public async Task<UserDto?> LoginUser(LoginUserDto loginDto)
    {
        if (string.IsNullOrWhiteSpace(loginDto.PhoneNumber) && string.IsNullOrWhiteSpace(loginDto.UserName))
            return null;

        var passwordHashed = loginDto.Password.EncodeToMd5();
        User? user = null;

        if (loginDto.PhoneNumber != null)
            user = await _context.Users
                .FirstOrDefaultAsync(u => u.PhoneNumber == loginDto.PhoneNumber && u.Password == passwordHashed);
        else
            user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == loginDto.UserName && u.Password == passwordHashed);

        if (user == null)
            return null;

        var userDto = new UserDto()
        {
            FullName = user.FullName,
            Password = user.Password,
            Role = user.Role,
            UserName = user.UserName,
            RegisterDate = user.CreationDate,
            UserId = user.Id
        };
        return userDto;
    }

    public async Task<UserDto?> GetUserById(long userId)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            return null;

        return new UserDto()
        {
            FullName = user.FullName,
            Password = user.Password,
            Role = user.Role,
            UserName = user.UserName,
            RegisterDate = user.CreationDate,
            UserId = user.Id,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email
        };
    }
    public async Task<UserDto?> GetUserByUserName(string userName)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.UserName == userName);

        if (user == null)
            return null;

        return new UserDto()
        {
            FullName = user.FullName,
            Password = user.Password,
            Role = user.Role,
            UserName = user.UserName,
            RegisterDate = user.CreationDate,
            UserId = user.Id,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email
        };
    }
    public async Task<UserFilterDto> GetUsersByFilter(int pageId, int take)
    {
        var users = _context.Users.OrderByDescending(d => d.Id);

        var skip = (pageId - 1) * take;
        var model = new UserFilterDto()
        {
            Users = await users.Skip(skip).Take(take).Select(user => new UserDto()
            {
                FullName = user.FullName,
                Password = user.Password,
                Role = user.Role,
                UserName = user.UserName,
                RegisterDate = user.CreationDate,
                UserId = user.Id
            }).ToListAsync()
        };
        model.GeneratePaging(users, take, pageId);
        return model;
    }

    public async Task<OperationResult> FollowUser(long userId, long targetUserId)
    {
        var isFollowed =
            await _context.UserFollowers.FirstOrDefaultAsync(f =>
                f.UserId == userId && f.FollowingUserId == targetUserId);

        if (isFollowed != null)
            return OperationResult.Success();

        _context.UserFollowers.Add(new UserFollower()
        {
            UserId = userId,
            FollowingUserId = targetUserId
        });
        await _context.SaveChangesAsync();
        return OperationResult.Success();
    }

    public async Task<OperationResult> UnFollowUser(long userId, long targetUserId)
    {
        var followed =
            await _context.UserFollowers.FirstOrDefaultAsync(f =>
                f.UserId == userId && f.FollowingUserId == targetUserId);

        if (followed == null)
            return OperationResult.Success();

        _context.UserFollowers.Remove(followed);
        await _context.SaveChangesAsync();
        return OperationResult.Success();
    }
}