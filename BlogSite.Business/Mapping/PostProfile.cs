using AutoMapper;
using BlogSite.API.Models;
using BlogSite.API.ViewModels.PostVMs;

namespace BlogSite.API.Mapping
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {

            CreateMap<Post, CreatePostVM>();
            CreateMap<CreatePostVM, Post>();
        }

    }
}
