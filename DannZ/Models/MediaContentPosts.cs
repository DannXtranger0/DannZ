namespace DannZ.Models
{
    public class MediaContentPosts
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string MediaUrl { get; set; }
        public string MediaPublicId { get; set; }

        public virtual Post Post { get; set; }
    }
}
