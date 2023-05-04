namespace BlogSite.API.Messages
{
    public class CommentUpdatedEvent : IMessage
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
    }
}
