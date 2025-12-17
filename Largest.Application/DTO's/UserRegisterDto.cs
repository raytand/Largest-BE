using System.ComponentModel.DataAnnotations;

namespace Largest.Application.DTOs
{
    public class UserRegisterDto
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        [MinLength(8)]
        public required string Password { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }
    }
}
