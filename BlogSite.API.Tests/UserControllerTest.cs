using BlogSite.API.Controllers;
using BlogSite.API.Models;
using BlogSite.Business.Concrete;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BlogSite.API.Tests
{
    public class UserControllerTest
    {
        private readonly Mock<UserService> _mockRepo;
        private readonly  UsersController _controller;
        private List<User> users; 
        public UserControllerTest()
        {
            _mockRepo = new Mock<UserService>();
            _controller = new UsersController(_mockRepo.Object);
            users = new List<User>()
            {
                new User { Id= Guid.Parse("CE9014C9-FCF5-4D42-A977-C6EC999EF2C4"), Email="mehmet@gmail.com", Password="123456", Token= "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJjZTkwMTRjOS1mY2Y1LTRkNDItYTk3Ny1jNmVjOTk5ZWYyYzQiLCJyb2xlIjoiVXNlciIsImVtYWlsIjoibWVobWV0c2FoaW5AZ21haWwuY29tIiwibmJmIjoxNjgzNTIyNTY3LCJleHAiOjE2ODM1MjYxNjcsImlhdCI6MTY4MzUyMjU2N30.RAPh0zYoJpaXwOnx5du-HEr4gS794xdLY_UvZgtpGmA", RefreshToken= "/2gc6/zR2i2r62hVFAdSwJu2MX0YkBIQ490tgN5Sw5xlfoSMPAZsapxi8BhalpJ0usGR6Tu0TMS7Sn0YYoAYHQ==", RefreshTokenExpiryTime=DateTime.UtcNow },

                new User { Id = Guid.Parse("CE9014C9-FCF5-4D42-A977-C6EC999EF2C5"), Email = "test@gmail.com", Password = "123456", Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJjZTkwMTRjOS1mY2Y1LTRkNDItYTk3Ny1jNmVjOTk5ZWYyYzQiLCJyb2xlIjoiVXNlciIsImVtYWlsIjoibWVobWV0c2FoaW5AZ21haWwuY29tIiwibmJmIjoxNjgzNTIyNTY3LCJleHAiOjE2ODM1MjYxNjcsImlhdCI6MTY4MzUyMjU2N30.RAPh0zYoJpaXwOnx5du-HEr4gS794xdLY_UvZgtpGmA", RefreshToken = "/2gc6/zR2i2r62hVFAdSwJu2MX0YkBIQ490tgN5Sw5xlfoSMPAZsapxi8BhalpJ0usGR6Tu0TMS7Sn0YYoAYHQ==", RefreshTokenExpiryTime = DateTime.UtcNow },
            };
        }

        [Fact]
        public async void GetUsers_ActionExecutes_ReturnOkResultWithUsers()
        {
            _mockRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(users);

            var result = await _controller.GetAsync();

            var okResult = Xunit.Assert.IsType<OkObjectResult>(result);

            var returnUsers = Assert.IsAssignableFrom<IEnumerable<User>>(okResult.Value);

            Assert.Equal<int>(2, returnUsers.ToList().Count);
        }

        [Theory]
        [InlineData("CE9014C9-FCF5-4D42-A977-C6EC999EF2C4")]
        public async void GetUsers_IdInValid_ReturnBadRequest(Guid userId)
        {
            User user = null;

            _mockRepo.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(user);

            var result = await _controller.GetUserByIdAsync(userId);

            Assert.IsType<BadRequestResult>(result);
        }
    }
}
