﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Messages.Events
{
    public class PostDeletedEvent
    {
        public Guid Id { get; set; }
    }
}