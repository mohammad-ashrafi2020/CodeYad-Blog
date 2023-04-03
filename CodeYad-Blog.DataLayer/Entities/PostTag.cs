using System.ComponentModel.DataAnnotations.Schema;

namespace CodeYad_Blog.DataLayer.Entities;

public class PostTag : BaseEntity
{
    public long TagId { get; set; }
    public long PostId { get; set; }


    [ForeignKey("TagId")]
    public Tag Tag { get; set; }

    [ForeignKey("PostId")]
    public Post Post { get; set; }
}