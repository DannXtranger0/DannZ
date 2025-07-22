namespace DannZ.Models.DTO.Post
{
    public class FeedPostDTO
    {
        public int userId { get; set; }
        public string? UserAvatarUrl { get; set; }
        public string? UserName { get; set; }
        public DateTime? UploadedDateTime { get; set; }
        public string? TextContent { get; set; }
        public List<string>? MultimediaUrl { get; set; }
    }
}
