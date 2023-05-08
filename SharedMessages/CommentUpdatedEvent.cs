using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedMessages
{
    public class CommentUpdatedEvent
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
    }
}
