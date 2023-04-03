using System.ComponentModel.DataAnnotations;

namespace CodeYad_Blog.DataLayer.Entities;

public class Notification : BaseEntity
{
    [MaxLength(100)]
    public string? Title { get; set; }
    public string Text { get; set; }
    public long UserId { get; set; }
    public bool IsSeen { get; set; }


    public User User { get; set; }
}