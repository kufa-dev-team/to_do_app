using System.ComponentModel.DataAnnotations;

namespace To_Do_App.DTOs
{
    public class UpdateCategoryDto
    {
        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }
        public string? Color { get; set; }
    }
}
