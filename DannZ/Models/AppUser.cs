using Microsoft.AspNetCore.Identity;

namespace DannZ.Models
{
    public class AppUser : IdentityUser
    {
        public string? Biography { get; set; }
        public string? AvatarUrl {  get; set; }
        public string? CoverUrl { get; set; }

    }
}
