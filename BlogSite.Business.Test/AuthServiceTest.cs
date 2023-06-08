using AutoMapper;
using BlogSite.Business.Authentication;
using BlogSite.Business.Concrete;
using BlogSite.DataAccsess.Abstract;
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
    public class AuthServiceTest
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private ILogger<AuthService> _logger;
        private ITokenHandler _tokenHandler;
        private IHttpContextAccessor _contextAccessor;
        public AuthServiceTest()
        {
            _userRepository = A.Fake<IUserRepository>();
            _mapper = A.Fake<IMapper>();
            _logger = A.Fake<ILogger<AuthService>>();
            _tokenHandler = A.Fake<ITokenHandler>();
            _contextAccessor = A.Fake<IHttpContextAccessor>();
        }
    }
}
