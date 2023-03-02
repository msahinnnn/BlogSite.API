using BlogSite.API.Models;
using BlogSite.API.ViewModels.CommentVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.DataAccsess.Abstract
{
    public interface ICommentRepository
    {
        List<Comment> GetAllComments();
        List<Comment> GetCommentsByPostId(Guid postId);
        Comment GetCommentById(Guid commentId);

        bool CreateComment(CreateCommentVM createCommentVM);
        bool UpdateComment(UpdateCommentVM updateCommentVM);
        bool DeleteComment(Guid commentId);

    }
}
