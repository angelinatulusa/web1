namespace web1.Models
{
    public class Article
    {
        public int Id { get; set; } //обязательно чтобы хотя бы первая буквы была больщой, тогда юудет воспринимать как ключевое слово и позволит создать таблицу
        public string Header { get; set; }
        public string Content { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
