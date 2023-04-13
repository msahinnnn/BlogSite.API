﻿using AutoMapper;
using BlogSite.API.Models;
using BlogSite.API.ViewModels.UserVMs;
using BlogSite.Business.Abstract;
using BlogSite.Business.Authentication;
using BlogSite.Business.Constants;
using BlogSite.Core.Utilities.Results;
using BlogSite.DataAccsess.Abstract;
using BlogSite.Entities.ViewModels.UserVMs;
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
        private IUserService _userService;
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private ILogger<AuthService> _logger;
        private ITokenHandler _tokenHandler;

        public AuthService(IUserService userService, IUserRepository userRepository, IMapper mapper, ILogger<AuthService> logger, ITokenHandler tokenHandler)
        {
            _userService = userService;
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _tokenHandler = tokenHandler;
        }

        public async Task<IDataResult<User>> RegisterAsync(CreateUserVM createUserVM)
        {
            createUserVM.Password =  BCrypt.Net.BCrypt.HashPassword(createUserVM.Password);
            var res = await _userService.CreateAsync(createUserVM);
            if (res.Success)
            {
                return new SuccessDataResult<User>(res.Data, res.Message);
            }
            _logger.LogError(UserAuthMessages.UserRegisterError);
            return new ErrorDataResult<User>(res.Data, UserAuthMessages.UserRegisterError);
        }

        public async Task<IDataResult<TokenDto>> LoginAsync(LoginUserVM loginUserVM)
        {
            var user = await _userService.EmailExists(loginUserVM.Email);
            if(user != null)
            {
                bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(loginUserVM.Password, user.Password);
                if (user != null && isPasswordCorrect)
                {

                    //TokenHandler tokenHandler = new TokenHandler(_userRepository);
                    user.Token = _tokenHandler.CreateToken(user);

                    var newAccsessToken = user.Token;
                    var newRefreshToken = _tokenHandler.CreateRefreshToken();
                    user.RefreshToken = newRefreshToken;
                    user.RefreshTokenExpiryTime = DateTime.Now.AddHours(1);

                    await _userRepository.UpdateAsync(user);

                    var tok = new TokenDto()
                    {
                        AccessToken = newAccsessToken,
                        RefreshToken = newRefreshToken,
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
            TokenHandler tokenHandler = new TokenHandler(_userRepository);
            ClaimsPrincipal principal = tokenHandler.GetPrincipalFromExpiredToken(accessToken);
            var userEmail = principal.Identity.Name;

            var user = await _userService.EmailExists(userEmail);

            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                _logger.LogError(UserAuthMessages.UserLoginError);
                return new ErrorDataResult<TokenDto>(new TokenDto(), UserAuthMessages.UserLoginError);
            }
                

            var newAccessToken = tokenHandler.CreateToken(user);
            var newRefreshToken = tokenHandler.CreateRefreshToken();
            user.RefreshToken = newRefreshToken;
            await _userRepository.UpdateAsync(user);

            var tok = new TokenDto()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
            };

            return new SuccessDataResult<TokenDto>(tok, UserAuthMessages.TokenCreated);
        }

    }
}