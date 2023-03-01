namespace BlogSite.API.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set;} = DateTime.Now;
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<Comment>? Comments { get; set;}
    }
}
