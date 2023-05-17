using Caching.Abstract;
using Caching.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caching.Concrete
{
    public class PostRepository : IPostRepository
    {
        public async Task<bool> CreateAsync(Post entity)
        {
            var conn = new SqlConnection("Data Source=mssql,1450; Initial Catalog=BlogSiteAppDB; Persist Security Info=True;User ID=SA; Password=mrMehmet123#; TrustServerCertificate=True;");
            var saveStatus = await conn.ExecuteAsync("INSERT INTO Posts (Id,Content,CreatedDate,PostId,UserId) VALUES(@Id,@Content,@CreatedDate,@PostId,@UserId)", entity);
            return saveStatus > 0 ? true : false;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var conn = new SqlConnection("Data Source=mssql,1450; Initial Catalog=BlogSiteAppDB; Persist Security Info=True;User ID=SA; Password=mrMehmet123#; TrustServerCertificate=True;");
            var status = await conn.ExecuteAsync("Delete from Posts where Id=@Id", new { Id = id });
            return status > 0 ? true : false;
        }

        public async Task<List<Post>> GetAllAsync()
        {
            var conn = new SqlConnection("Data Source=mssql,1450; Initial Catalog=BlogSiteAppDB; Persist Security Info=True;User ID=SA; Password=mrMehmet123#; TrustServerCertificate=True;");
            IEnumerable<Post> res = await conn.QueryAsync<Post>("sp_get_posts");
            return res.ToList();
            //var posts = await conn.QueryAsync<Post>("Select * from Posts");
            //return posts.ToList();
        }

        public async Task<Post> GetByIdAsync(Guid id)
        {
            var conn = new SqlConnection("Data Source=mssql,1450; Initial Catalog=BlogSiteAppDB; Persist Security Info=True;User ID=SA; Password=mrMehmet123#; TrustServerCertificate=True;");
            IEnumerable<Post> res = await conn.QueryAsync<Post>("sp_get_post", new { Id = id });
            return res.FirstOrDefault();
            //var post = (await conn.QueryAsync<Post>("Select * from Posts where Id=@Id", new { Id = id })).SingleOrDefault();
            //return post;
        }

        public async Task<bool> UpdateAsync(Post entity)
        {
            var conn = new SqlConnection("Data Source=mssql,1450; Initial Catalog=BlogSiteAppDB; Persist Security Info=True;User ID=SA; Password=mrMehmet123#; TrustServerCertificate=True;");
            var status = await conn.ExecuteAsync("Update Posts set @Id,@Content,@CreatedDate,@PostId,@Posts,@UserId where Id=@Id", entity);
            return status > 0 ? true : false;
        }

    }
}
