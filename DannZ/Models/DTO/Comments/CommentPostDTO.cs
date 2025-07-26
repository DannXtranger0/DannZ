namespace DannZ.Models.DTO.Comments
{
    public class CommentPostDTO
    {
        public int? commentId { get; set; }
        public int PostId { get; set; }
        public int  UserId { get; set; }
        public string? CommentContent { get; set; }
        public string? UserAvatarUrl { get; set; }
        public string? UserName { get; set; }
        public DateTime? UploadedDateTime { get; set; }
    }

}
