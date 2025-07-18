namespace DannZ.Models.DTO.Post
{
    public class CreatePostDTO
    {
        public string? TextContent { get; set; }
        public List<IFormFile>? MultimediaList { get; set; }

        
    }
}
