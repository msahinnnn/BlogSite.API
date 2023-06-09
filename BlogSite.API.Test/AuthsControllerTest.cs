using AutoMapper;
using BlogSite.API.Controllers;
using BlogSite.API.Models;
using BlogSite.API.ViewModels.UserVMs;
using BlogSite.Business.Abstract;
using BlogSite.Business.Concrete;
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
    public class AuthsControllerTest
    {
        public IAuthService _authService;
        public AuthsController _authsController;
        public IMapper _mapper;

        public AuthsControllerTest()
        {
            _authService = A.Fake<IAuthService>();
            _mapper = A.Fake<IMapper>();
            _authsController = new AuthsController(_authService);
        }

        [Fact]
        public async void LoginUser_ReturnsOK()
        {
            var userVM = new LoginUserVM()
            {
                Email = "test@gmail.com",
                Password = "password",
            };

            var user = A.Fake<User>();
            A.CallTo(() => _mapper.Map<User>(userVM)).Returns(user);

            var token = A.Fake<TokenDto>();
            var res = A.CallTo(() => _authService.LoginAsync(userVM)).Returns(token);
            Assert.NotNull(res);

            var result = await _authsController.Login(userVM);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void RegisterUser_ReturnsOK()
        {
            var userVM = new CreateUserVM()
            {
                Email = "test@gmail.com",
                Password = "password",
            };

            var user = A.Fake<User>();
            A.CallTo(() => _mapper.Map<User>(userVM)).Returns(user);

            var res = A.CallTo(() => _authService.RegisterAsync(userVM)).Returns(user);
            Assert.NotNull(res);

            var result = await _authsController.Register(userVM);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void RefreshLogin_ReturnsOK()
        {
            var token = A.Fake<TokenDto>();
            var res = A.CallTo(() => _authService.RefreshAsync(token)).Returns(token);
            Assert.NotNull(res);

            var result = await _authsController.Refresh(token);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
