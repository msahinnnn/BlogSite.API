using BlogSite.API.Models;

namespace BlogSite.API.ViewModels.PostVMs
{
    public class CreatePostVM
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
