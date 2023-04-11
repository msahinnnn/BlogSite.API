using BlogSite.Core.Entities;

namespace BlogSite.API.Models
{
    public class User : IBaseEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool Status { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
}
