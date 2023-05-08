using BlogSite.API.Models;
using BlogSite.DataAccsess.Abstract;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.DataAccsess.Concrete.Dapper
{
    public class UserDapperRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;

        public UserDapperRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<User> CreateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<User> CheckUserEmailExistsAsync(string mail)
        {
            throw new NotImplementedException();
        }

        public bool CheckUserRefreshTokenExists(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
