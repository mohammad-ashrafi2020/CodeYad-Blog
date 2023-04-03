using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeYad_Blog.DataLayer.Entities;

public class Tag : BaseEntity
{
    [MaxLength(150)]
    public string Title { get; set; }
    public long UserCreatorId { get; set; }

    [ForeignKey("UserCreatorId")]
    public User User { get; set; }
}