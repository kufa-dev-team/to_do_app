namespace To_Do_App.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
