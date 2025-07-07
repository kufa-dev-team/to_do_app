namespace To_Do_App.Models
{
    public class Todo
    {
        public int todo_id { get; set; }
        public int user_id { get; set; }
        public int? category_id { get; set; }
        public required string title { get; set; }
        public string? description { get; set; }
        public bool completed { get; set; }
        public int priority { get; set; }
        public DateTime? due_date { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime? completion_timestamp { get; set; }

    }
}
