namespace BlogSite.API.Messages
{
    public class PostUpdatedEvent : IMessage
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid UserId { get; set; }
    }
}
