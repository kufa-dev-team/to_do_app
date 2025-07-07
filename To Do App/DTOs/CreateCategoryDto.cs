using System.ComponentModel.DataAnnotations;

namespace To_Do_App.DTOs
{
    public class CreateCategoryDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        public string? Color { get; set; }
    }
}
