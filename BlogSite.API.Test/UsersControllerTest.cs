using AutoMapper;
using BlogSite.API.Controllers;
using BlogSite.API.Models;
using BlogSite.API.ViewModels.UserVMs;
using BlogSite.Business.Abstract;
using BlogSite.Entities.ViewModels.UserVMs;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.API.Test
{
    public class UsersControllerTest
    {
        public IUserService _userService;
        public UsersController _userController;
        public IMapper _mapper;
        public UsersControllerTest()
        {
            _userService = A.Fake<IUserService>();
            _mapper = A.Fake<IMapper>();
            _userController = new UsersController(_userService);
        }

        [Fact]
        public async void GetUsers_ReturnsOK()
        {
            var users = A.Fake<List<User>>();
            var res = A.CallTo(() => _userService.GetAllAsync()).Returns(users);
            Assert.NotNull(res);

            var result = await _userController.GetAsync();
            Assert.NotNull(result);

        }

        [Fact]
        public async void GetUserById_ReturnsOK()
        {
            var user = A.Fake<User>();
            var res = A.CallTo(() => _userService.GetByIdAsync(user.Id)).Returns(user);
            Assert.NotNull(res);

            var result = await _userController.GetUserByIdAsync(user.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public async void CreateUser_ReturnsOK()
        {
            var userVM = new CreateUserVM()
            {
                Email = "test@gmail.com",
                Password = "password",
            };

            var user = A.Fake<User>();
            A.CallTo(() => _mapper.Map<User>(userVM)).Returns(user);

            var res = A.CallTo(() => _userService.CreateAsync(userVM)).Returns(user);
            Assert.NotNull(res);

            var result = await _userController.GetUserByIdAsync(user.Id);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void DeleteUser_ReturnsOK()
        {
            var user = A.Fake<User>();
            A.CallTo(() => _userService.DeleteAsync(user.Id)).Returns(true);

            var result = await _userController.DeleteAsync(user.Id);
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async void UpdateUser_ReturnsOK()
        {
            var userVM = new UpdateUserVM()
            {
                Email = "test@gmail.com",
            };

            var user = A.Fake<User>();
            A.CallTo(() => _mapper.Map<User>(userVM)).Returns(user);

            var res = A.CallTo(() => _userService.UpdateAsync(userVM, user.Id)).Returns(true);
            Assert.NotNull(res);

            var result = await _userController.UpdateAsync(userVM, user.Id);
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
    }
}
