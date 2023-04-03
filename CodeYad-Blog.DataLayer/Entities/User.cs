using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CodeYad_Blog.DataLayer.Entities;

[Index("Email",IsUnique = true)]
[Index("PhoneNumber", IsUnique = true)]
[Index("UserName", IsUnique = true)]
public class User : BaseEntity
{
    [Required]
    [MaxLength(102)]
    public string UserName { get; set; }

    [MaxLength(12)]
    public string? PhoneNumber { get; set; }

    [MaxLength(100)]
    public string? Email { get; set; }

    [MaxLength(100)]
    public string? FullName { get; set; }

    [Required]
    public string Password { get; set; }
    public UserRole Role { get; set; }
    public string? Avatar { get; set; }
    public string? AboutMe { get; set; }



    #region Relations
    public ICollection<Post> Posts { get; set; }
    public ICollection<PostComment> PostComments { get; set; }

    [InverseProperty("User")]
    public ICollection<UserFollower> Followers { get; set; }

    [InverseProperty("FollowingUser")]
    public ICollection<UserFollower> Followings { get; set; }

    #endregion
}

public enum UserRole
{
    Admin,
    User,
    Writer
}