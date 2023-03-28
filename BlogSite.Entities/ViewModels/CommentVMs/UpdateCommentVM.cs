using BlogSite.API.Models;
using BlogSite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Entities.ViewModels.CommentVMs
{
    public class UpdateCommentVM : IVM<Comment>
    {
        public Guid PostId { get; set; }
        public string Content { get; set; }
    }
}
