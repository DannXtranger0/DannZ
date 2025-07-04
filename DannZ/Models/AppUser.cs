using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DannZ.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        public string Name {  get; set; }
        public string? Biography { get; set; }
        public string? AvatarUrl {  get; set; }
        public string? CoverUrl { get; set; }

    }
}
