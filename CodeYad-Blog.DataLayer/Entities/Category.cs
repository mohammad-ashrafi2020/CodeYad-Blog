using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CodeYad_Blog.DataLayer.Entities;


[Index("Slug", IsUnique = true)]
public class Category : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required]
    [MaxLength(100)]
    public string Slug { get; set; }

    public SeoData SeoData { get; set; }

    [MaxLength(100)]
    public string? ImageName { get; set; }
    public ICollection<Post> Posts { get; set; }
}