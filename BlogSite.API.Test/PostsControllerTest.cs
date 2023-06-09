using AutoMapper;
using BlogSite.API.Controllers;
using BlogSite.API.Models;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.Business.Abstract;
using BlogSite.Entities.ViewModels.PostVMs;
using BlogSite.Messages.Events;
using FakeItEasy;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.API.Test
{
    public class PostsControllerTest
    {
        public IPostCacheService _postCacheService;
        public IPostService _postService;
        public IPublishEndpoint _publishEndpoint;
        public IMapper _mapper;
        public PostsController _postsController;

        public PostsControllerTest()
        {
            _postCacheService = A.Fake<IPostCacheService>();
            _postService = A.Fake<IPostService>();
            _publishEndpoint = A.Fake<IPublishEndpoint>();
            _mapper = A.Fake<IMapper>();
            _postsController = new PostsController(_postService, _publishEndpoint, _postCacheService);
        }

        [Fact]
        public async void GetPost_ReturnsOK()
        {
            var posts = A.Fake<List<Post>>();
            A.CallTo(() => _postCacheService.GetAsync()).Returns(posts);

            var result = await _postsController.GetAsync();
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetPostByIdPost_ReturnsOK()
        {
            var post = A.Fake<Post>();
            A.CallTo(() => _postCacheService.GetByIdAsync(post.Id)).Returns(post);

            var result = await _postsController.GetPostByIdAsync(post.Id);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetAllPostsByUserIdPost_ReturnsOK()
        {
            var user = A.Fake<User>();
            var post = A.Fake<List<Post>>();
            A.CallTo(() => _postService.GetPostsByUserIdAsync(user.Id)).Returns(post);

            var result = await _postsController.GetAllPostsByUserIdAsync(user.Id);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void CreatePost_ReturnsOK()
        {
            var postVM = new CreatePostVM()
            {
                Title = "Test Title",
                Content = "Test Content",
            };

            var post = A.Fake<Post>();

            A.CallTo(() => _mapper.Map<Post>(postVM)).Returns(post);

            A.CallTo(() => _postService.CreateAsync(postVM)).Returns(post);
            Assert.NotNull(post);

            var result = await _postsController.CreateAsync(postVM);
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);

        }

        [Fact]
        public async void UpdatePost_ReturnsOK()
        {
            var postVM = new UpdatePostVM()
            {
                Title = "Test Title",
                Content = "Test Content",
            };

            var post = A.Fake<Post>();

            A.CallTo(() => _mapper.Map<Post>(postVM)).Returns(post);
            var id = Guid.NewGuid();
            A.CallTo(() => _postService.UpdateAsync(postVM, post.Id)).Returns(true);

            var result = await _postsController.UpdateAsync(postVM, post.Id);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async void DeletePost_ReturnsOK()
        {
            var post = A.Fake<Post>();

            A.CallTo(() => _postService.DeleteAsync(post.Id)).Returns(true);
            Assert.NotNull(post);

            var result = await _postsController.DeleteAsync(post.Id);
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
    }
}
