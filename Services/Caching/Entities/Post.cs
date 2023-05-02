using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caching.Entities
{
    [Dapper.Contrib.Extensions.Table("Posts")]
    public class Post : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public Guid UserId { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
