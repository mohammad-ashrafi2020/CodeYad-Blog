using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CodeYad_Blog.DataLayer.Entities
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}