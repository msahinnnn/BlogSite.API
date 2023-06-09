using BlogSite.API.Models;
using BlogSite.Core.Entities;

namespace BlogSite.API.ViewModels.CommentVMs
{
    public class CreateCommentVM : IVM<Comment>
    {
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public string Content { get; set; }
    }
}
