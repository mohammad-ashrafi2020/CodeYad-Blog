using System;
using System.Linq;
using CodeYad_Blog.CoreLayer.DTOs.Users;
using CodeYad_Blog.CoreLayer.Utilities;
using CodeYad_Blog.DataLayer.Context;
using CodeYad_Blog.DataLayer.Entities;

namespace CodeYad_Blog.CoreLayer.Services.Users
{
    public class UserService : IUserService
    {
        private readonly BlogContext _context;

        public UserService(BlogContext context)
        {
            _context = context;
        }

        public OperationResult EditUser(EditUserDto command)
        {
            var user = _context.Users.FirstOrDefault(c => c.Id == command.UserId);
            if (user == null)
                return OperationResult.NotFound();

            user.FullName = command.FullName;
            user.Role = command.Role;
            _context.SaveChanges();
            return OperationResult.Success();
        }

        public OperationResult RegisterUser(UserRegisterDto registerDto)
        {
            var isUserNameExist = _context.Users.Any(u => u.UserName == registerDto.UserName);

            if (isUserNameExist)
                return OperationResult.Error("نام کاربری تکراری است");

            var passwordHash = registerDto.Password.EncodeToMd5();
            _context.Users.Add(new User()
            {
                FullName = registerDto.Fullname,
                IsDelete = false,
                UserName = registerDto.UserName,
                Role = UserRole.User,
                CreationDate = DateTime.Now,
                Password = passwordHash
            });
            _context.SaveChanges();
            return OperationResult.Success();
        }

        public UserDto LoginUser(LoginUserDto loginDto)
        {
            var passwordHashed = loginDto.Password.EncodeToMd5();
            var user = _context.Users
                .FirstOrDefault(u => u.UserName == loginDto.UserName && u.Password == passwordHashed);

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

        public UserDto GetUserById(int userId)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Id == userId);
            if (user == null)
                return null;
            return new UserDto()
            {
                FullName = user.FullName,
                Password = user.Password,
                Role = user.Role,
                UserName = user.UserName,
                RegisterDate = user.CreationDate,
                UserId = user.Id
            };
        }

        public UserFilterDto GetUsersByFilter(int pageId, int take)
        {
            var users = _context.Users.OrderByDescending(d => d.Id)
                .Where(c => !c.IsDelete);

            var skip = (pageId - 1) * take;
            var model= new UserFilterDto()
            {
                Users = users.Skip(skip).Take(take).Select(user => new UserDto()
                {
                    FullName = user.FullName,
                    Password = user.Password,
                    Role = user.Role,
                    UserName = user.UserName,
                    RegisterDate = user.CreationDate,
                    UserId = user.Id
                }).ToList()
            };
            model.GeneratePaging(users, take,pageId);
            return model;
        }
    }
}