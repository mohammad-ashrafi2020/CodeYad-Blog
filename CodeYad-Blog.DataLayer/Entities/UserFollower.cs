using System.ComponentModel.DataAnnotations.Schema;

namespace CodeYad_Blog.DataLayer.Entities;

public class UserFollower:BaseEntity
{
    public long UserId { get; set; }
    public long FollowingUserId { get; set; }


    [ForeignKey("UserId")]
    public User User { get; set; }

    [ForeignKey("FollowingUserId")]
    public User FollowingUser { get; set; }
}