using BlogSite.API.Models;
using BlogSite.DataAccsess.Abstract;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.DataAccsess.Concrete.Dapper
{
    public class PostDapperRepository : IPostRepository
    {
        private readonly IConfiguration _configuration;

        public PostDapperRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Post> CreateAsync(Post entity)
        {
            var conn = new SqlConnection("Data Source=localhost,1450; Initial Catalog=BlogSiteAppDB; Persist Security Info=True;User ID=SA; Password=mrMehmet123#; TrustServerCertificate=True;");
            var saveStatus = await conn.ExecuteAsync("INSERT INTO Posts (Id,Content,CreateTime,PostId,Posts,UserId) VALUES(@Id,@Content,@CreateTime,@PostId,@Posts,@UserId)", entity);
            return saveStatus > 0 ? entity : null;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var conn = new SqlConnection("Data Source=localhost,1450; Initial Catalog=BlogSiteAppDB; Persist Security Info=True;User ID=SA; Password=mrMehmet123#; TrustServerCertificate=True;");
            var status = await conn.ExecuteAsync("Delete from Posts where Id=@Id", new { Id = id });
            return status > 0 ? true : false;
        }

        public async Task<List<Post>> GetAllAsync()
        {
            var conn = new SqlConnection("Data Source=localhost,1450; Initial Catalog=BlogSiteAppDB; Persist Security Info=True;User ID=SA; Password=mrMehmet123#; TrustServerCertificate=True;");
            var posts = await conn.QueryAsync<Post>("Select * from Posts");
            return posts.ToList();
        }

        public async Task<Post> GetByIdAsync(Guid id)
        {
            var conn = new SqlConnection("Data Source=localhost,1450; Initial Catalog=BlogSiteAppDB; Persist Security Info=True;User ID=SA; Password=mrMehmet123#; TrustServerCertificate=True;");
            var post = (await conn.QueryAsync<Post>("Select * from Posts where Id=@Id", new { Id = id })).SingleOrDefault();
            return post;
        }

        public async Task<List<Post>> GetPostsByUserIdAsync(Guid userId)
        {
            var conn = new SqlConnection("Data Source=localhost,1450; Initial Catalog=BlogSiteAppDB; Persist Security Info=True;User ID=SA; Password=mrMehmet123#; TrustServerCertificate=True;");
            var posts = (await conn.QueryAsync<Post>("Select * from Posts where UserId=@UserId", new { UserId = userId }));
            return posts.ToList();
        }

        public async Task<bool> UpdateAsync(Post entity)
        {
            var conn = new SqlConnection("Data Source=localhost,1450; Initial Catalog=BlogSiteAppDB; Persist Security Info=True;User ID=SA; Password=mrMehmet123#; TrustServerCertificate=True;");
            var status = await conn.ExecuteAsync("Update Posts set @Id,@Content,@CreateTime,@PostId,@Posts,@UserId where Id=@Id", entity);
            return status > 0 ? true : false;
        }
    }
}
