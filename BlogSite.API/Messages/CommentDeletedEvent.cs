namespace BlogSite.API.Messages
{
    public class CommentDeletedEvent : IMessage
    {
        public Guid Id { get; set; }
    }
}
