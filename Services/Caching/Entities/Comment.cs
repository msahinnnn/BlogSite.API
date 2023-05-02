using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caching.Entities
{
    [Dapper.Contrib.Extensions.Table("Comments")]
    public class Comment : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public Guid UserId { get; set; }
    }
}
