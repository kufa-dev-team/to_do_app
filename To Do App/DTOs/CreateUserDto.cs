using System.ComponentModel.DataAnnotations;

namespace To_Do_App.DTOs
{
    public class CreateUserDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [MinLength(8)]
        public required string Password { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
