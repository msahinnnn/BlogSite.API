using AutoMapper;
using BlogSite.API.Models;
using BlogSite.API.Validations;
using BlogSite.API.ViewModels.UserVMs;
using BlogSite.Business.Abstract;
using BlogSite.Business.Authentication;
using BlogSite.Business.Constants;
using BlogSite.Business.Validations;
using BlogSite.Core.Utilities.Results;
using BlogSite.DataAccsess.Abstract;
using BlogSite.Entities.ViewModels.UserVMs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Business.Concrete
{
    public class AuthService : IAuthService
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private ILogger<AuthService> _logger;
        private ITokenHandler _tokenHandler;
        private IHttpContextAccessor _contextAccessor;


        public AuthService(IUserRepository userRepository, IMapper mapper, ILogger<AuthService> logger, ITokenHandler tokenHandler, IHttpContextAccessor contextAccessor)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _tokenHandler = tokenHandler;
            _contextAccessor = contextAccessor;
        }

        public async Task<IDataResult<User>> RegisterAsync(CreateUserVM createUserVM)
        {
            ValidationTool.Validate(new UserValidator(), createUserVM);
            createUserVM.Password = BCrypt.Net.BCrypt.HashPassword(createUserVM.Password);
            User user = _mapper.Map<User>(createUserVM);
            user.Id = Guid.NewGuid();
            user.Token = _tokenHandler.CreateToken(user, UserRoles.User, 1);
            user.RefreshToken = _tokenHandler.CreateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.Now.AddHours(1);
            var check = await _userRepository.CheckUserEmailExistsAsync(user.Email);
            if (check is null)
            {

                var res = await _userRepository.CreateAsync(user);
                if (res is not null)
                {
                    return new SuccessDataResult<User>(user, UserMessages.UserAdded);
                }
                _logger.LogError(UserMessages.UserAddedError);
                return new ErrorDataResult<User>(user, UserMessages.UserAddedError);
            }
            _logger.LogError(UserMessages.UserAldreadyExistsError);
            return new ErrorDataResult<User>(user, UserMessages.UserAldreadyExistsError);
        }

        public async Task<IDataResult<TokenDto>> LoginAsync(LoginUserVM loginUserVM)
        {
            var user = await _userRepository.CheckUserEmailExistsAsync(loginUserVM.Email);
            if (user != null)
            {
                bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(loginUserVM.Password, user.Password);
                if (user != null && isPasswordCorrect)
                {
                    user.Token = _tokenHandler.CreateToken(user, UserRoles.User, 1);

                    var newAccsessToken = user.Token;
                    var newRefreshToken = _tokenHandler.CreateRefreshToken();
                    user.RefreshToken = newRefreshToken;
                    user.RefreshTokenExpiryTime = DateTime.Now.AddHours(1);

                    await _userRepository.UpdateAsync(user);

                    var tok = new TokenDto()
                    {
                        AccessToken = newAccsessToken,
                        RefreshToken = newRefreshToken,
                        AccessTokenExpiryTime = DateTime.Now.AddHours(1)
                    };
                    return new SuccessDataResult<TokenDto>(tok, UserAuthMessages.TokenCreated);
                }
                _logger.LogError(UserAuthMessages.UserLoginError);
                return new ErrorDataResult<TokenDto>(new TokenDto(), UserAuthMessages.UserLoginError);
            }
            _logger.LogError(UserAuthMessages.UserLoginError);
            return new ErrorDataResult<TokenDto>(new TokenDto(), UserAuthMessages.UserLoginError);
        }

        public async Task<IDataResult<TokenDto>> RefreshAsync(TokenDto tokenDto)
        {
            if (tokenDto is null)
            {
                _logger.LogError(UserAuthMessages.UserLoginError);
                return new ErrorDataResult<TokenDto>(new TokenDto(), UserAuthMessages.UserLoginError);
            }

            string accessToken = tokenDto.AccessToken;
            string refreshToken = tokenDto.RefreshToken;
            ClaimsPrincipal principal = _tokenHandler.GetPrincipalFromExpiredToken(accessToken);
            var userEmail = principal.Identity.Name;
            var user = await _userRepository.CheckUserEmailExistsAsync(userEmail);

            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                _logger.LogError(UserAuthMessages.UserLoginError);
                return new ErrorDataResult<TokenDto>(new TokenDto(), UserAuthMessages.UserLoginError);
            }

            var role = GetCurrentUserRole();
            var newAccessToken = _tokenHandler.CreateToken(user, role, 1);
            var newRefreshToken = _tokenHandler.CreateRefreshToken();
            user.RefreshToken = newRefreshToken;
            await _userRepository.UpdateAsync(user);

            var tok = new TokenDto()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                AccessTokenExpiryTime = DateTime.Now.AddHours(1)
            };

            return new SuccessDataResult<TokenDto>(tok, UserAuthMessages.TokenCreated);
        }

        public string GetCurrentUserId()
        {
            return _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public string GetCurrentUserMail()
        {
            return _contextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Email).Value;

        }

        public string GetCurrentUserRole()
        {
            return _contextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Role).Value;
        }


    }
}
