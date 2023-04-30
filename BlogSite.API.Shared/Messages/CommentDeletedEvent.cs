using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.API.Shared.Messages
{
    public class CommentDeletedEvent : IMessage
    {
        public Guid Id { get; set; }

    }
}
