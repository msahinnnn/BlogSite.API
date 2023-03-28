using BlogSite.API.Models;
using BlogSite.Core.Entities;

namespace BlogSite.API.ViewModels.PostVMs
{
    public class CreatePostVM : IVM<Post>
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
