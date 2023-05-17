using BlogSite.API.Models;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.DataAccsess.Abstract;
using BlogSite.Entities.ViewModels.CommentVMs;
using BlogSite.Entities.ViewModels.PostVMs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.DataAccsess.Concrete.AdoNet
{
    public class PostRepository : IPostRepository
    {
        private readonly IConfiguration _configuration;

        public PostRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Post>> GetAllAsync()
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlLocalConnection"));
            await con.OpenAsync();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Posts", con);
            DataTable dt = new DataTable();
            List<Post> posts = new List<Post>();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Post post = new Post();
                post.Id = Guid.Parse(dt.Rows[i]["Id"].ToString());
                post.Title = dt.Rows[i]["Title"].ToString();
                post.Content = dt.Rows[i]["Content"].ToString();
                post.CreatedDate = DateTime.Parse(dt.Rows[i]["CreatedDate"].ToString());
                post.UserId = Guid.Parse(dt.Rows[i]["UserId"].ToString());
                posts.Add(post);
            }
            await con.CloseAsync();
            await con.DisposeAsync();
            return posts;
        }

        public async Task<Post> GetByIdAsync(Guid id)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlLocalConnection"));
            await con.OpenAsync();
            SqlCommand cmd = new SqlCommand("Select * from Posts where Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            Post post = new Post();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            post.Id = id;
            post.Title = ds.Tables[0].Rows[0]["Title"].ToString();
            post.Content = ds.Tables[0].Rows[0]["Content"].ToString();
            post.CreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
            post.UserId = Guid.Parse(ds.Tables[0].Rows[0]["UserId"].ToString());
            await con.CloseAsync();
            await con.DisposeAsync();
            return post;
        }

        public async Task<List<Post>> GetPostsByUserIdAsync(Guid userId)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlLocalConnection"));
            await con.OpenAsync();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Posts where UserId=@UserId", con);
            da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
            DataTable dt = new DataTable();
            List<Post> posts = new List<Post>();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Post post = new Post();
                post.Id = Guid.Parse(dt.Rows[i]["Id"].ToString());
                post.Title = dt.Rows[i]["Title"].ToString();
                post.Content = dt.Rows[i]["Content"].ToString();
                post.CreatedDate = DateTime.Parse(dt.Rows[i]["CreatedDate"].ToString());
                post.UserId = Guid.Parse(dt.Rows[i]["UserId"].ToString());
                posts.Add(post);
            }
            await con.CloseAsync();
            await con.DisposeAsync();
            return posts;
        }

        public async Task<Post> CreateAsync(Post entity)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlLocalConnection"));
            SqlCommand cmd = new SqlCommand("Insert into Posts (Id, Title, Content, CreatedDate, UserId) values (@Id, @Title, @Content, @CreatedDate, @UserId)", con);
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@Title", entity.Title);
            cmd.Parameters.AddWithValue("@Content", entity.Content);
            cmd.Parameters.AddWithValue("@CreatedDate", entity.CreatedDate);
            cmd.Parameters.AddWithValue("@UserId", entity.UserId);
            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            await con.CloseAsync();
            await con.DisposeAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlLocalConnection"));
            SqlCommand cmd = new SqlCommand("Delete from Posts where Id=@Id", con);
            cmd.Parameters.AddWithValue("Id", id);
            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            await con.CloseAsync();
            await con.DisposeAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(Post entity)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlLocalConnection"));
            SqlCommand cmd = new SqlCommand("Update Posts Set Title=@Title, Content=@Content where Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@Title", entity.Title);
            cmd.Parameters.AddWithValue("@Content", entity.Content);
            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            await con.CloseAsync();
            await con.DisposeAsync();
            return true;
        }
    }
}
