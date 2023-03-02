using BlogSite.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.DataAccsess.Abstract
{
    public interface IPostRepository
    {
        List<Post> GetAllPosts();
        List<Post> GetPostsByUserId(Guid userId);
        Post GetPostById(Guid postId);
    }
}
