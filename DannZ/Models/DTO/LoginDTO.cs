using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DannZ.Models.DTO
{
    
    public class LoginDTO
    {
        [Required]
        public string Email {  get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }

    }
}
