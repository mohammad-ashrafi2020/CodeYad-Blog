using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeYad_Blog.DataLayer.Entities
{
    public class PostComment: BaseEntity
    {
     
        public long UserId { get; set; }
        public long PostId { get; set; }
        [Required]
        public string Text { get; set; }

        public long? ParentId { get; set; }

        #region Relations

        [ForeignKey("ParentId")]
        public List<PostComment> Answers { get; set; }

        [ForeignKey("PostId")]
        public Post Post { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        #endregion
    }
}
