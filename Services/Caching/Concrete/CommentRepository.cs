using Caching.Abstract;
using Caching.Entities;
using Dapper;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caching.Concrete
{
    public class CommentRepository : ICommentRepository
    {
        public async Task<bool> CreateAsync(Comment entity)
        {
            var conn = new SqlConnection("Data Source=localhost,1450; Initial Catalog=BlogSiteAppDB; Persist Security Info=True;User ID=SA; Password=mrMehmet123#; TrustServerCertificate=True;");
            var saveStatus = await conn.ExecuteAsync("INSERT INTO Comment (Id,Content,CreateTime,PostId,Posts,UserId) VALUES(@Id,@Content,@CreateTime,@PostId,@Posts,@UserId)", entity);
            return saveStatus > 0 ? true : false;
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

        public async Task<bool> UpdateAsync(Comment entity)
        {
            var conn = new SqlConnection("Data Source=localhost,1450; Initial Catalog=BlogSiteAppDB; Persist Security Info=True;User ID=SA; Password=mrMehmet123#; TrustServerCertificate=True;");
            var status = await conn.ExecuteAsync("Update Comments set @Id,@Content,@CreateTime,@PostId,@Posts,@UserId where Id=@Id", entity);
            return status > 0 ? true : false;
        }
    }
}
