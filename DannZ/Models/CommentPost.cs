namespace DannZ.Models
{
    public class CommentPost
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string CommentContent { get; set; }
        public DateTime UploadedDateTime { get; set; }
        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
