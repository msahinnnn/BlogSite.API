using AutoMapper;
using BlogSite.API.Models;
using BlogSite.API.Validations;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.API.ViewModels.UserVMs;
using BlogSite.Business.Abstract;
using BlogSite.Business.Concrete;
using BlogSite.Business.Validations;
using BlogSite.DataAccsess.Abstract;
using BlogSite.DataAccsess.Concrete.AdoNet;
using BlogSite.Entities.ViewModels.UserVMs;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Business.Test
{
    public class PostServiceTest
    {
        private IPostRepository _postRepository;
        private IMapper _mapper;
        private IAuthService _authService;
        private IPostService _postService;
        public PostServiceTest()
        {
            _postRepository = A.Fake<IPostRepository>();
            _mapper = A.Fake<IMapper>();
            _authService = A.Fake<IAuthService>();
            _postService = new PostService(_postRepository, _mapper, _authService);
        }

        [Fact]
        public async void GetPosts_ReturnsPosts()
        {
            var posts = A.Fake<List<Post>>();
            A.CallTo(() => _postRepository.GetAllAsync()).Returns(posts);
            var result = await _postService.GetAllAsync();
            Assert.IsAssignableFrom<List<Post>>(result);
        }

        [Fact]
        public async void GetPostById_ReturnsPost()
        {
            var id = Guid.NewGuid();
            var post = A.Fake<Post>();
            A.CallTo(() => _postRepository.GetByIdAsync(id)).Returns(post);

            var result = await _postService.GetByIdAsync(id);
            Assert.IsAssignableFrom<Post>(result);
        }

        [Fact]
        public async void GetPostsByUserId_ReturnsPostsPerUser()
        {
            var id = Guid.NewGuid();
            var posts = A.Fake<List<Post>>();
            A.CallTo(() => _postRepository.GetPostsByUserIdAsync(id)).Returns(posts);

            var result = await _postService.GetPostsByUserIdAsync(id);
            Assert.IsAssignableFrom<List<Post>>(result);
        }

        [Fact]
        public async void CreatePost_ReturnsPost()
        {
            var id = Guid.NewGuid();
            var postVM = new CreatePostVM()
            {
                Title = "Test Title",
                Content = "Test Content",
            };
            var post = A.Fake<Post>();

            ValidationTool.Validate(new PostValidator(), postVM);

            A.CallTo(() => _postRepository.GetByIdAsync(id)).Returns(post);
            A.CallTo(() => _mapper.Map<Post>(postVM)).Returns(post);
            A.CallTo(() => _postRepository.CreateAsync(post)).Returns(post);

            var result = await _postService.CreateAsync(postVM);
            Assert.NotNull(result);

        }

        [Fact]
        public async void UpdatePost_ReturnsTrue()
        {
            var id = Guid.NewGuid();
            var postVM = new CreatePostVM()
            {
                Title = "Test Title",
                Content = "Test Content",
            };
            var post = A.Fake<Post>();

            A.CallTo(() => _postRepository.GetByIdAsync(id)).Returns(post);
            A.CallTo(() => _mapper.Map<Post>(postVM)).Returns(post);
            A.CallTo(() => _postRepository.UpdateAsync(post)).Returns(true);

            var result = await _postService.UpdateAsync(postVM, id);
            Assert.True(result);
        }

        [Fact]
        public async void DeletePost_ReturnsTrue()
        {
            var id = Guid.NewGuid();
            var post = A.Fake<Post>();

            A.CallTo(() => _postRepository.GetByIdAsync(id)).Returns(post);
            A.CallTo(() => _postRepository.DeleteAsync(id)).Returns(true);

            var result = await _postService.DeleteAsync(id);
            Assert.True(result);
        }
    }
}
