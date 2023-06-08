using AutoMapper;
using BlogSite.API.Models;
using BlogSite.API.Validations;
using BlogSite.API.ViewModels.UserVMs;
using BlogSite.Business.Abstract;
using BlogSite.Business.Authentication;
using BlogSite.Business.Concrete;
using BlogSite.Business.Validations;
using BlogSite.DataAccsess.Abstract;
using BlogSite.Entities.ViewModels.UserVMs;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Business.Test
{
    public class UserServiceTest
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private ILogger<UserService> _logger;
        private ITokenHandler _tokenHandler;
        private IAuthService _authService;
        private IUserService _userService;
        public UserServiceTest()
        {
            _userRepository = A.Fake<IUserRepository>();
            _mapper = A.Fake<IMapper>();
            _logger = A.Fake<ILogger<UserService>>();
            _tokenHandler = A.Fake<ITokenHandler>();
            _authService = A.Fake<IAuthService>();
            _userService = new UserService(_mapper, _userRepository, _logger, _tokenHandler, _authService);
        }

        [Fact]
        public async void GetUsers_ReturnsUsers()
        {
            var users = A.Fake<List<User>>();
            A.CallTo(() => _userRepository.GetAllAsync()).Returns(users);
            var result = await _userRepository.GetAllAsync();
            Assert.IsAssignableFrom<List<User>>(result);
        }

        [Fact]
        public async void GetUserById_ReturnsUser()
        {
            var id = Guid.NewGuid();
            var user = A.Fake<User>();
            A.CallTo(() => _userRepository.GetByIdAsync(id)).Returns(user);

            var result = await _userRepository.GetByIdAsync(id);
            Assert.IsAssignableFrom<User>(result);
        }

        [Fact]
        public async void CreateUser_ReturnsUser()
        {
            var id = Guid.NewGuid();
            var userVM = new CreateUserVM()
            {
                Email = "Test123@gmail.com",
                Password = "Test123",
            };
            var user = A.Fake<User>();

            ValidationTool.Validate(new UserValidator(), userVM);

            A.CallTo(() => _userRepository.GetByIdAsync(id)).Returns(user);
            A.CallTo(() => _mapper.Map<User>(userVM)).Returns(user);
            A.CallTo(() => _userRepository.CreateAsync(user)).Returns(user);

            var result = await _userRepository.CreateAsync(user);
            Assert.NotNull(result);
           
        }

        [Fact]
        public async void UpdateUser_ReturnsTrue()
        {
            var id = Guid.NewGuid();
            var userVM = new UpdateUserVM()
            {
                Email = "Test123@gmail.com",
            };
            var user = A.Fake<User>();

            A.CallTo(() => _userRepository.GetByIdAsync(id)).Returns(user);
            A.CallTo(() => _mapper.Map<User>(userVM)).Returns(user);
            A.CallTo(() => _userRepository.UpdateAsync(user)).Returns(true);

            var result = await _userRepository.UpdateAsync(user);
            Assert.True(result);
        }

        [Fact]
        public async void DeleteUser_ReturnsTrue()
        {
            var id = Guid.NewGuid();
            var user = A.Fake<User>();

            A.CallTo(() => _userRepository.GetByIdAsync(id)).Returns(user);
            A.CallTo(() => _userRepository.DeleteAsync(id)).Returns(true);

            var result = await _userRepository.DeleteAsync(id);
            Assert.True(result);
        }


    }
}
