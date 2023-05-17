using BlogSite.API.Controllers;
using BlogSite.API.Models;
using BlogSite.API.ViewModels.UserVMs;
using BlogSite.Business.Abstract;
using BlogSite.Entities.ViewModels.UserVMs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using BadRequestResult = Microsoft.AspNetCore.Mvc.BadRequestResult;
using OkResult = Microsoft.AspNetCore.Mvc.OkResult;

namespace BlogSite.API.Test
{
    public class UserControllerTest
    {
        private readonly Mock<IUserService> _mockRepo;
        private readonly UsersController _controller;
        private List<User> users;
        public UserControllerTest()
        {
            _mockRepo = new Mock<IUserService>();
            _controller = new UsersController(_mockRepo.Object);
            users = new List<User>()
            {
                new User { Id= Guid.Parse("3A912910-2340-4EE1-9D53-A1897E063DC7"), Email="mehmet@gmail.com", Password="123456", Token= "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJjZTkwMTRjOS1mY2Y1LTRkNDItYTk3Ny1jNmVjOTk5ZWYyYzQiLCJyb2xlIjoiVXNlciIsImVtYWlsIjoibWVobWV0c2FoaW5AZ21haWwuY29tIiwibmJmIjoxNjgzNTIyNTY3LCJleHAiOjE2ODM1MjYxNjcsImlhdCI6MTY4MzUyMjU2N30.RAPh0zYoJpaXwOnx5du-HEr4gS794xdLY_UvZgtpGmA", RefreshToken= "/2gc6/zR2i2r62hVFAdSwJu2MX0YkBIQ490tgN5Sw5xlfoSMPAZsapxi8BhalpJ0usGR6Tu0TMS7Sn0YYoAYHQ==", RefreshTokenExpiryTime=DateTime.UtcNow },

                new User { Id = Guid.Parse("CE9014C9-FCF5-4D42-A977-C6EC999EF2C5"), Email = "test@gmail.com", Password = "123456", Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJjZTkwMTRjOS1mY2Y1LTRkNDItYTk3Ny1jNmVjOTk5ZWYyYzQiLCJyb2xlIjoiVXNlciIsImVtYWlsIjoibWVobWV0c2FoaW5AZ21haWwuY29tIiwibmJmIjoxNjgzNTIyNTY3LCJleHAiOjE2ODM1MjYxNjcsImlhdCI6MTY4MzUyMjU2N30.RAPh0zYoJpaXwOnx5du-HEr4gS794xdLY_UvZgtpGmA", RefreshToken = "/2gc6/zR2i2r62hVFAdSwJu2MX0YkBIQ490tgN5Sw5xlfoSMPAZsapxi8BhalpJ0usGR6Tu0TMS7Sn0YYoAYHQ==", RefreshTokenExpiryTime = DateTime.UtcNow },
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
