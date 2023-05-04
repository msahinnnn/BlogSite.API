using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Models
{
    public class PostDeletedEvent : IMessage
    {
        public Guid Id { get; set; }
    }
}
