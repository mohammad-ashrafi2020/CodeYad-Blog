using System;

namespace CodeYad_Blog.CoreLayer.DTOs.Comments
{
    public class CommentDto
    {
        public long CommentId { get; set; }
        public string UserFullName { get; set; }
        public long PostId { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
    }
}