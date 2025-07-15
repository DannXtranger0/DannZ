using System.ComponentModel.DataAnnotations;

namespace DannZ.Models.DTO.Account
{
    public class ProfileDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Biography { get; set; }
        public string? AvatarUrl { get; set; }
        public string? CoverUrl{ get; set; }
    }
}
