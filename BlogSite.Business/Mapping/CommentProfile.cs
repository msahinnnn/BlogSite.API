using AutoMapper;
using BlogSite.API.Models;
using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.Entities.ViewModels.CommentVMs;

namespace BlogSite.API.Mapping
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CreateCommentVM>();
            CreateMap<CreateCommentVM, Comment>();

            CreateMap<Comment, UpdateCommentVM>();
            CreateMap<UpdateCommentVM, Comment>();
        }
    }
}
