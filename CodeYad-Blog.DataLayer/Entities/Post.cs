using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeYad_Blog.DataLayer.Entities
{

    [Index("Slug", IsUnique = true)]
    public class Post: BaseEntity
    {
        public long UserId { get; set; }
        public long CategoryId { get; set; }
        [Required]
        [MaxLength(300)]
        public string Title { get; set; }

        [Required]
        [MaxLength(400)]
        public string Slug { get; set; }
        [Required]
        public string Description { get; set; }
        public string? ImageName { get; set; }
        public int Visit { get; set; }
        public bool IsSpecial { get; set; }
        public string Tags { get; set; }
        public SeoData SeoData { get; set; }

        #region Relations
        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public ICollection<PostComment> PostComments { get; set; }
        #endregion
    }
}
