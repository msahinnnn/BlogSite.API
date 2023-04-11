using BlogSite.API.Models;
using BlogSite.API.Validations;
using BlogSite.API.ViewModels.UserVMs;
using BlogSite.Business.Abstract;
using BlogSite.Business.Constants;
using BlogSite.Business.Validations;
using BlogSite.Core.Entities.Concrete;
using BlogSite.Core.Security.Hashing;
using BlogSite.Core.Security.JWT;
using BlogSite.Core.Utilities.Results;
using BlogSite.DataAccsess.Abstract;
using BlogSite.Entities.ViewModels.UserVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Business.Concrete
{
    public class AuthService : IAuthService
    {
        private ITokenHelper _tokenHelper;
        private IUserRepository _userRepository;

        public AuthService(ITokenHelper tokenHelper, IUserService userService, IUserRepository userRepository)
        {
            _tokenHelper = tokenHelper;
            _userRepository = userRepository;
        }

        public async Task<IDataResult<AccessToken>> CreateAccessTokenAsync(User user)
        {
            var claims = await _userRepository.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, UserAuthMessages.TokenCreated);
        }

        public Task<List<OperationClaim>> GetClaims(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<User>> LoginAsync(LoginUserVM loginUserVM)
        {
            var userToCheck = await _userRepository.CheckUserEmailExistsAsync(loginUserVM.Email);
            if (userToCheck is null)
            {
                return new ErrorDataResult<User>(userToCheck, UserAuthMessages.UserLoginError);
            }

            if (!HashingHelper.VerifyPasswordHash(loginUserVM.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new DataResult<User>(userToCheck, false, UserAuthMessages.UserLoginError);
            }

            return new DataResult<User>(userToCheck, true, UserAuthMessages.UserLogined);
        }

        public async Task<IDataResult<User>> RegisterAsync(CreateUserVM createUserVM, string password)
        {
            ValidationTool.Validate(new UserValidator(), createUserVM);
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = createUserVM.Email,
                FirstName = createUserVM.FirstName,
                LastName = createUserVM.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            var check = await _userRepository.CheckUserEmailExistsAsync(user.Email);
            if (check is null)
            {
                var res = await _userRepository.CreateAsync(user);
                if (res is not null)
                {
                    return new SuccessDataResult<User>(user, UserAuthMessages.UserRegistered);
                }
                return new ErrorDataResult<User>(user, UserAuthMessages.UserRegisterError);
            }
            return new ErrorDataResult<User>(user, UserAuthMessages.UserAlreadyExistsError);
        }
    }
}
