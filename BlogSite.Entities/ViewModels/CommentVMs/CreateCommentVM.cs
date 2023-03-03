namespace BlogSite.API.ViewModels.CommentVMs
{
    public class CreateCommentVM
    {
        public Guid PostId { get; set; }
        public string Content { get; set; }
    }
}
