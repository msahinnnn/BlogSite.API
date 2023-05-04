namespace BlogSite.API.Messages
{
    public class PostDeletedEvent : IMessage
    {
        public Guid Id { get; set; }
    }
}
