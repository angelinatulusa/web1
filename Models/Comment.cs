namespace web1.Models
{
    public class Comment //после добавления каждой новой модели надо добавить public DbSet<...> ... { get; set; } в ApplicationDbContext
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public int ArticleId { get; set; }
    }
}
