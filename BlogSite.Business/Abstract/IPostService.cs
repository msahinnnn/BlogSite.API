using BlogSite.API.Models;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.Core.Business;
using BlogSite.Core.Utilities.Results;
using BlogSite.Entities.ViewModels.PostVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Business.Abstract
{
    public interface IPostService : IEntityService<Post>
    {
        //Task<IDataResult<List<Post>>> GetAllPostsAsync();
        Task<IDataResult<List<Post>>> GetPostsByUserIdAsync(Guid userId);
        //Task<IDataResult<Post>> GetPostByIdAsync(Guid postId);

        //Task<IResult> CreatePostAsync(CreatePostVM createPostVM);
        //Task<IResult> UpdatePostAsync(UpdatePostVM updatePostVM, Guid postId);
        //Task<IResult> DeletePostAsync(Guid postId);


    }
}
