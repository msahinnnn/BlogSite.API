using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.API.Shared.Messages
{
    public class PostChangedEvent
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string CreatedDate { get; set; }
        public string UserId { get; set; }
    }
}
