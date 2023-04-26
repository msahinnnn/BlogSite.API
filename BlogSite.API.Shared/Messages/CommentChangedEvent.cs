using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.API.Shared.Messages
{
    public class CommentChangedEvent
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string CreateTime { get; set; } 
        public string PostId { get; set; }
        public string UserId { get; set; }
    }
}
