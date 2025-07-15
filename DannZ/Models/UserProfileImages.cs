using System.ComponentModel.DataAnnotations;

namespace DannZ.Models
{
    public class UserProfileImages
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? AvatarUrl { get; set; }
        public string? AvatarPublicId { get; set; }

        public string? CoverUrl { get; set; }
        public string? CoverPublicId { get; set; }

        public virtual User? User { get; set; }
    }
}
