using AutoMapper;
using BlogSite.API.Models;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.Entities.ViewModels.PostVMs;

namespace BlogSite.API.Mapping
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {

            CreateMap<Post, CreatePostVM>();
            CreateMap<CreatePostVM, Post>();

            CreateMap<Post, UpdatePostVM>();
            CreateMap<UpdatePostVM, Post>();
        }

    }
}
