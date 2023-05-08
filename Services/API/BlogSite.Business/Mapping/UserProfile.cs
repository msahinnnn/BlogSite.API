using AutoMapper;
using BlogSite.API.Models;
using BlogSite.API.ViewModels.UserVMs;
using BlogSite.Entities.ViewModels.UserVMs;

namespace BlogSite.API.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, CreateUserVM>();
            CreateMap<CreateUserVM, User>();

            CreateMap<User, UpdateUserVM>();
            CreateMap<UpdateUserVM, User>();
        }
    }
}
