using System.ComponentModel.DataAnnotations;

namespace To_Do_App.DTOs
{
    public class UpdateTodoDto
    {
        public int? CategoryId { get; set; }

        [Required]
        [MaxLength(255)]
        public required string Title { get; set; }
        public string? Description { get; set; }
        public bool Completed { get; set; }

        [Range(1, 3)]
        public int Priority { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
