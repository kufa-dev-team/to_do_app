namespace To_Do_App.Models
{
    public class Category
    {
        public int category_id { get; set; }
        public int user_id { get; set; }
        public required string name { get; set; }
        public string? color { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
