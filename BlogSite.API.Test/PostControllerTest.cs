using BlogSite.API.Controllers;
using BlogSite.API.Models;
using BlogSite.API.ViewModels.UserVMs;
using BlogSite.Business.Abstract;
using BlogSite.Entities.ViewModels.UserVMs;
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
        private List<Post> posts;
        public PostControllerTest()
        {
            _mockRepo = new Mock<IPostService>();
            _controller = new PostsController(_mockRepo.Object);
            posts = new List<Post>()
            {
                
            };
              
        }

        //-----------GET ALL---------------
        [Fact]
        public async void GetUsers_ActionExecutes_ReturnOkResultWithUsers()
        {
            _mockRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(users);

            var result = await _controller.GetAsync();

            var okResult = Xunit.Assert.IsType<OkObjectResult>(result);

            var returnUsers = Assert.IsAssignableFrom<IEnumerable<User>>(okResult.Value);

            Assert.Equal<int>(2, returnUsers.ToList().Count);
        }

        //-------------GET BY ID------------
        [Theory]
        [InlineData("3A912910-2340-4EE1-9D53-A1897E063DC7")]
        [InlineData("CE9014C9-FCF5-4D42-A977-C6EC999EF2C5")]
        public async void GetUser_IdValid_ReturnOkResult(Guid userId)
        {
            var user = users.First(x => x.Id == userId);

            _mockRepo.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(user);

            var result = await _controller.GetUserByIdAsync(userId);

            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnUser = Assert.IsType<User>(okResult.Value);

            Assert.Equal(userId, returnUser.Id);
            Assert.Equal(user.Email, returnUser.Email);
        }

        [Theory]
        [InlineData("3A912910-2340-4EE1-9D53-A1897E063DC7")]
        public async void GetUser_IdInValid_ReturnBadRequest(Guid userId)
        {
            User user = null;

            _mockRepo.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(user);

            var result = await _controller.GetUserByIdAsync(userId);

            Assert.IsType<BadRequestResult>(result);
        }

        //---------CREATE-------------
        [Fact]
        public async void PostUser_ActionExecutes_ReturnBadRequest()
        {
            var userVM = new CreateUserVM()
            {
                Email = "abc@gmail.com",
                Password = "123456",
            };

            _mockRepo.Setup(x => x.CreateAsync(userVM));

            var result = await _controller.CreateAsync(userVM);

            var okObjectResult = Assert.IsType<BadRequestResult>(result);


        }


        //---------DELETE-------------
        [Theory]
        [InlineData("00000000-FCF5-4D42-A977-C6EC999EF2C4")]
        public async void DeleteUser_IdInValid_BadRequest(Guid userId)
        {
            User user = null;

            _mockRepo.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(user);

            var result = await _controller.DeleteAsync(userId);

            Assert.IsType<BadRequestResult>(result);
        }


        [Theory]
        [InlineData("3A912910-2340-4EE1-9D53-A1897E063DC7")]
        public async void PutUser_IdIsNotEqualUser_ReturnBadRequestResult(Guid userId)
        {
            var user = users.First(x => x.Id == userId);
            var userVM = new UpdateUserVM()
            {
                Email = user.Email,
            };
            var result = await _controller.UpdateAsync(userVM, Guid.Parse("00000000-FCF5-4D42-A977-C6EC999EF2C4"));

            var badRequestResult = Assert.IsType<BadRequestResult>(result);
        }

    }
}
