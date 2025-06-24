using System.ComponentModel.DataAnnotations;

namespace To_Do_App.DTOs
{
    public class CreateTodoDto
    {
        [Required]
        public int UserId { get; set; }
        public int? CategoryId { get; set; }

        [Required]
        [MaxLength(255)]
        public required string Title { get; set; }
        public string? Description { get; set; }

        [Range(1, 3)] // Priority 1=Low, 2=Medium, 3=High
        public int Priority { get; set; } = 2; // Medium as default
        public DateTime? DueDate { get; set; }

    }
}
