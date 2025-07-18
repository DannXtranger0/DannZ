namespace DannZ.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string TextContent { get; set; }
        public DateTime UploadedDateTime { get; set; }
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<CommentPost>? Comment_Posts { get; set; }
        public virtual ICollection<MediaContentPosts>? MediaContents { get; set; }
    }
}
