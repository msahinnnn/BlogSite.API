using AutoMapper;
using BlogSite.API.Models;
using BlogSite.API.Validations;
using BlogSite.API.ViewModels.UserVMs;
using BlogSite.Business.Abstract;
using BlogSite.Business.Authentication;
using BlogSite.Business.Concrete;
using BlogSite.Business.Constants;
using BlogSite.Business.Validations;
using BlogSite.DataAccsess.Abstract;
using BlogSite.Entities.ViewModels.UserVMs;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Business.Test
{
    public class AuthServiceTest
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private ILogger<AuthService> _logger;
        private ITokenHandler _tokenHandler;
        private IHttpContextAccessor _contextAccessor;
        private IAuthService _authService;
        public AuthServiceTest()
        {
            _userRepository = A.Fake<IUserRepository>();
            _mapper = A.Fake<IMapper>();
            _logger = A.Fake<ILogger<AuthService>>();
            _tokenHandler = A.Fake<ITokenHandler>();
            _contextAccessor = A.Fake<IHttpContextAccessor>();
            _authService = new AuthService(_userRepository, _mapper, _logger, _tokenHandler, _contextAccessor);
        }

        [Fact]
        public async void RegisterAsync()
        {
            var userVM = new CreateUserVM()
            {
                Email = "Test123@gmail.com",
                Password = "Test123",
            };
            userVM.Password = BCrypt.Net.BCrypt.HashPassword(userVM.Password);

            var user = A.Fake<User>();

            user.Token = A.CallTo(() => _tokenHandler.CreateToken(user, UserRoles.User, 1)).ToString();
            user.RefreshToken = A.CallTo(() => _tokenHandler.CreateRefreshToken()).ToString();
            user.RefreshTokenExpiryTime = DateTime.Now.AddHours(1);

            A.CallTo(() => _mapper.Map<User>(userVM)).Returns(user);

            A.CallTo(() => _userRepository.CheckUserEmailExistsAsync(userVM.Email)).Returns(user);

            var check = A.CallTo(() => _userRepository.CreateAsync(user)).Returns(user);
            Assert.NotNull(check);

            var result = await _authService.RegisterAsync(userVM);
            //result.Should().BeOfType<User>();

        }

        [Fact]
        public async void LoginAsync()
        {
            var id = Guid.NewGuid();
            var userVM = new LoginUserVM()
            {
                Email = "Test123@gmail.com",
                Password = "Test123",
            };
            var user = A.Fake<User>();


            var check = A.CallTo(() => _userRepository.CheckUserEmailExistsAsync(userVM.Email)).Returns(user);
            Assert.NotNull(check);

            A.CallTo(() => _mapper.Map<User>(userVM)).Returns(user);

            var userToken = A.CallTo(() => _tokenHandler.CreateToken(user, UserRoles.User, 1));
            user.Token = userToken.ToString();
            var token = new TokenDto()
            {
                AccessToken = userToken.ToString(),
                RefreshToken = A.CallTo(() => _tokenHandler.CreateRefreshToken()).ToString(),
                AccessTokenExpiryTime = DateTime.Now.AddHours(1)
            };

            A.CallTo(() => _userRepository.CreateAsync(user)).Returns(user);

            var result = await _userRepository.CreateAsync(user);        
            Assert.NotNull(result);
        }

        [Fact]
        public async void RefreshAsync()
        {
            var tokenDto = A.Fake<TokenDto>();
            var prcpl = A.Fake<ClaimsPrincipal>();

            A.CallTo(() => _tokenHandler.GetPrincipalFromExpiredToken(tokenDto.AccessToken)).Returns(prcpl);

            var userEmail = prcpl.Identity.Name;
            var user = await _userRepository.CheckUserEmailExistsAsync(userEmail);
            Assert.NotNull(user);
            Assert.NotEqual(user.RefreshToken, tokenDto.RefreshToken);

            var role = _authService.GetCurrentUserRole();
            var newAccessToken = A.CallTo(() => _tokenHandler.CreateToken(user, role, 1)).ToString();
            var newRefreshToken = A.CallTo(() => _tokenHandler.CreateRefreshToken()).ToString();
            Assert.NotNull(newAccessToken);
            Assert.NotNull(newRefreshToken);
            user.RefreshToken = newRefreshToken;

            A.CallTo(() => _userRepository.UpdateAsync(user)).Returns(true);

            var newToken = new TokenDto()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                AccessTokenExpiryTime = DateTime.Now.AddHours(1)
            };

            var result = await _authService.RefreshAsync(newToken);
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetCurrentUserId()
        {
            var id = _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value;
            var res = _authService.GetCurrentUserId();
            Assert.Equal(id, res);
        }

        [Fact]
        public async void GetCurrentUserMail()
        {
            var mail = _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email).Value;
            var res = _authService.GetCurrentUserMail();
            Assert.Equal(mail, res);
        }

        [Fact]
        public async void GetCurrentUserRole()
        {
            var role = _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role).Value;
            var res = _authService.GetCurrentUserRole();
            Assert.Equal(role, res);
        }
    }
}

