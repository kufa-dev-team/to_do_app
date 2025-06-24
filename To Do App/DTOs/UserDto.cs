namespace To_Do_App.DTOs
{
    public class UserDto
    {
        public int user_id { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public required string email { get; set; }
        public DateTime created_at { get; set; }
    }
}
