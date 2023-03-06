using BlogSite.Core.Entities;

namespace BlogSite.API.Models
{
    public class User : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
}
