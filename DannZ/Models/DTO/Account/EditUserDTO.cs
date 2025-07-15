using System.ComponentModel.DataAnnotations;

namespace DannZ.Models.DTO.Account
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
        public IFormFile? AvatarUrl  { get; set; }
        public IFormFile? CoverUrl { get; set; }
    }
}
