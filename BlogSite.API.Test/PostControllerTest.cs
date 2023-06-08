using BlogSite.API.Controllers;
using BlogSite.API.Models;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.API.ViewModels.UserVMs;
using BlogSite.Business.Abstract;
using BlogSite.Business.Concrete;
using BlogSite.Entities.ViewModels.PostVMs;
using BlogSite.Entities.ViewModels.UserVMs;
using BlogSite.Messages.Events;
using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.API.Test
{
    public class PostControllerTest
    {
        private readonly Mock<IPostService> _mockRepo;
        private readonly PostsController _controller;
        private readonly Mock<IPublishEndpoint> _publishEndpoint;
        private readonly Mock<IAuthService> _authService;
        private readonly Mock<IPostCacheService> _postCacheService;
        public PostControllerTest()
        {
            _mockRepo = new Mock<IPostService>();
            _publishEndpoint = new Mock<IPublishEndpoint>();
            _authService = new Mock<IAuthService>();
            _postCacheService = new Mock<IPostCacheService>();
            _controller = new PostsController(_mockRepo.Object, _publishEndpoint.Object, _authService.Object , _postCacheService.Object);
        }

        [Fact]
        public async void GetPosts_ActionExecutes_ReturnOkResultWithUsers()
        {
            //var postId = Guid.NewGuid();
            //var userId = Guid.NewGuid();
            //List<Post> posts = new List<Post>()
            //{
            //    new Post() { Id = postId, Content = "Test", CreatedDate = DateTime.UtcNow, Title = "Test", UserId = userId },
            //    new Post() { Id = postId, Content = "Test", CreatedDate = DateTime.UtcNow, Title = "Test", UserId = userId }
            // };
            //_postCacheService.Setup(x => x.GetAsync()).ReturnsAsync(posts);

            //var okRes = await _controller.GetAsync();

            //var okResult = Xunit.Assert.IsType<OkObjectResult>(okRes);

            //var returnUsers = Assert.IsAssignableFrom<IEnumerable<Post>>(okResult.Value);
        }

        [Fact]
        public async void GetPostById_ActionExecutes_ReturnOkResultWithUsers()
        {
            //var postId = Guid.NewGuid();
            //var userId = Guid.NewGuid();
            //var post = new Post() { Id = postId, Content = "Test", CreatedDate = DateTime.UtcNow, Title = "Test", UserId = userId };
            //_postCacheService.Setup(x => x.GetByIdAsync(postId)).ReturnsAsync(post);

            //var res = await _controller.GetPostByIdAsync(postId);
            //Assert.IsType<OkObjectResult>(res);
        }

        [Fact]
        public async void PostUser_ActionExecutes_ReturnBadRequest()
        {
            var postVM = new CreatePostVM()
            {
                Title = "Test",
                Content = "Test",
            };

            _mockRepo.Setup(x => x.CreateAsync(postVM));
      
            var result = await _controller.CreateAsync(postVM);
            var okObjectResult = Assert.IsType<BadRequestResult>(result);


        }


        //---------DELETE-------------
        [Theory]
        [InlineData("00000000-FCF5-4D42-A977-C6EC999EF2C4")]
        public async void DeletePost_IdInValid_BadRequest(Guid postId)
        {
            Post post = null;

            _mockRepo.Setup(x => x.GetByIdAsync(postId)).ReturnsAsync(post);

            var result = await _controller.DeleteAsync(postId);

            Assert.IsType<BadRequestResult>(result);
        }


        [Theory]
        [InlineData("3A912910-2340-4EE1-9D53-A1897E063DC7")]
        public async void PutPost_IdIsNotEqualPost_ReturnBadRequestResult(Guid postId)
        {
            var postVM = new UpdatePostVM()
            {
                Title= "Test",
                Content = "Test",
            };
            _mockRepo.Setup(x => x.UpdateAsync(postVM, postId)).ReturnsAsync(true);
            var result = await _controller.UpdateAsync(postVM, postId);

            var badRequestResult = Assert.IsType<BadRequestResult>(result);
        }
    }
}
