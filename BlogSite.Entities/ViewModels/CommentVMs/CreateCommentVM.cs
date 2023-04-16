using BlogSite.API.Models;
using BlogSite.Core.Entities;

namespace BlogSite.API.ViewModels.CommentVMs
{
    public class CreateCommentVM : IVM<Comment>
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; }
    }
}
