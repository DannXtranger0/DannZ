﻿using System.ComponentModel.DataAnnotations;

namespace DannZ.Models.DTO
{
    public class RegisterDTO
    {
        [Required]
        public int Id{ get; set; }

        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public IFormFile? Avatar { get; set; }
    }
}
