using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caching.Messages
{
    public class PostDeletedEvent : IMessage
    {
        public Guid Id { get; set; }
    }
}
