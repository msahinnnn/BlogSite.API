using BlogSite.API.Models;
using BlogSite.DataAccsess.Abstract;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.DataAccsess.Concrete.Dapper
{
    public class CommentDapperRepository : ICommentRepository
    {
        private readonly IConfiguration _configuration;

        public CommentDapperRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Comment> CreateAsync(Comment entity)
        {
            var conn = new SqlConnection("Data Source=localhost,1450; Initial Catalog=BlogSiteAppDB; Persist Security Info=True;User ID=SA; Password=mrMehmet123#; TrustServerCertificate=True;");
            var saveStatus = await conn.ExecuteAsync("INSERT INTO Comment (Id,Content,CreateTime,PostId,Posts,UserId) VALUES(@Id,@Content,@CreateTime,@PostId,@Posts,@UserId)", entity);
            return saveStatus > 0 ? entity : null;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var conn = new SqlConnection("Data Source=localhost,1450; Initial Catalog=BlogSiteAppDB; Persist Security Info=True;User ID=SA; Password=mrMehmet123#; TrustServerCertificate=True;");
            var status = await conn.ExecuteAsync("Delete from Comments where Id=@Id", new { Id = id });
            return status > 0 ? true : false;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            var conn = new SqlConnection("Data Source=localhost,1450; Initial Catalog=BlogSiteAppDB; Persist Security Info=True;User ID=SA; Password=mrMehmet123#; TrustServerCertificate=True;");
            var comments = await conn.QueryAsync<Comment>("Select * from Comments");
            return comments.ToList();
        }

        public async Task<Comment> GetByIdAsync(Guid id)
        {
            var conn = new SqlConnection("Data Source=localhost,1450; Initial Catalog=BlogSiteAppDB; Persist Security Info=True;User ID=SA; Password=mrMehmet123#; TrustServerCertificate=True;");
            var comment = (await conn.QueryAsync<Comment>("Select * from Comments where Id=@Id", new { Id = id })).SingleOrDefault();
            return comment;
        }

        public async Task<List<Comment>> GetCommentsByPostIdAsync(Guid postId)
        {
            var conn = new SqlConnection("Data Source=localhost,1450; Initial Catalog=BlogSiteAppDB; Persist Security Info=True;User ID=SA; Password=mrMehmet123#; TrustServerCertificate=True;");
            var comments = (await conn.QueryAsync<Comment>("Select * from Comments where PostId=@PostId", new { PostId = postId }));
            return comments.ToList();
        }

        public async Task<bool> UpdateAsync(Comment entity)
        {
            var conn = new SqlConnection("Data Source=localhost,1450; Initial Catalog=BlogSiteAppDB; Persist Security Info=True;User ID=SA; Password=mrMehmet123#; TrustServerCertificate=True;");
            var status = await conn.ExecuteAsync("Update Comments set @Id,@Content,@CreateTime,@PostId,@Posts,@UserId where Id=@Id", entity);
            return status > 0 ? true : false;
        }
    }
}
