namespace BlogSite.API.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public Guid PostId { get; set; }
        public Post Post { get; set; }
    }
}
