using System.ComponentModel.DataAnnotations;

namespace DannZ.Models.DTO
{
    public class EditUserDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string? AvatarUrl { get; set; }
        public string? CoverUrl { get; set; }
    }
}
