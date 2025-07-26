namespace DannZ.Models.DTO.Comments
{
    public class CreateCommentPostDTO
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string? CommentContent { get; set; }
    }
}
