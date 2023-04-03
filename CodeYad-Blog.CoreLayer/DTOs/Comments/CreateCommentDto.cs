namespace CodeYad_Blog.CoreLayer.DTOs.Comments
{
    public class CreateCommentDto
    {
        public long UserId { get; set; }
        public long PostId { get; set; }
        public string Text { get; set; }
        public long? ParentId { get; set; }
    }
}