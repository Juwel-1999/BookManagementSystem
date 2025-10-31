namespace Book_Management_System.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Book>? Books { get; set; }
    }
}
