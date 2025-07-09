using System.ComponentModel.DataAnnotations;

namespace DannZ.Models
{
    public class User
    {
        public int  Id { get; set; }
        [Required]
        public string Name {  get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string? Biography { get; set; }
        public string? AvatarUrl { get; set; }
        public string? CoverUrl { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
