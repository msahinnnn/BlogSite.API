using BlogSite.API.Models;
using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.DataAccsess.Abstract;
using BlogSite.Entities.ViewModels.CommentVMs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.DataAccsess.Concrete.AdoNet
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IConfiguration _configuration;

        public CommentRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlLocalConnection"));
            await con.OpenAsync();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Comments", con);
            DataTable dt = new DataTable();
            List<Comment> comments = new List<Comment>();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Comment comment = new Comment();
                comment.Id = Guid.Parse(dt.Rows[i]["Id"].ToString());
                comment.Content = dt.Rows[i]["Content"].ToString();
                comment.CreateTime = DateTime.Parse(dt.Rows[i]["CreateTime"].ToString());
                comments.Add(comment);
            }
            await con.CloseAsync();
            await con.DisposeAsync();
            return comments;
        }

        public async Task<Comment> GetByIdAsync(Guid id)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlLocalConnection"));
            await con.OpenAsync();
            SqlCommand cmd = new SqlCommand("Select * from Comments where Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            Comment comment = new Comment();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            comment.Id = id;
            comment.PostId = Guid.Parse(ds.Tables[0].Rows[0]["PostId"].ToString());
            comment.Content = ds.Tables[0].Rows[0]["Content"].ToString();
            comment.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
            await con.CloseAsync();
            await con.DisposeAsync();
            return comment;
        }

        public async Task<List<Comment>> GetCommentsByPostIdAsync(Guid postId)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlLocalConnection"));
            await con.OpenAsync();
            SqlDataAdapter da = new SqlDataAdapter($"Select * from Comments where PostId=@PostId", con);
            da.SelectCommand.Parameters.AddWithValue("@PostId", postId);
            DataTable dt = new DataTable();
            List<Comment> comments = new List<Comment>();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Comment comment = new Comment();
                comment.Id = Guid.Parse(dt.Rows[i]["Id"].ToString());
                comment.Content = dt.Rows[i]["Content"].ToString();
                comment.CreateTime = DateTime.Parse(dt.Rows[i]["CreateTime"].ToString());
                comments.Add(comment);
            }
            await con.CloseAsync();
            await con.DisposeAsync();
            return comments;
        }

        public async Task<Comment> CreateAsync(Comment entity)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlLocalConnection"));
            SqlCommand cmd = new SqlCommand("Insert into Comments (Id, Content, CreateTime, PostId) values (@Id, @Content, @CreateTime, @PostId)", con);
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@Content", entity.Content);
            cmd.Parameters.AddWithValue("@CreateTime", entity.CreateTime);
            cmd.Parameters.AddWithValue("@PostId", entity.PostId);
            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            await con.CloseAsync();
            await con.DisposeAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlLocalConnection"));
            SqlCommand cmd = new SqlCommand("Delete from Comments where Id=@Id", con);
            cmd.Parameters.AddWithValue("Id", id);
            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            await con.CloseAsync();
            await con.DisposeAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(Comment entity)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlLocalConnection"));
            SqlCommand cmd = new SqlCommand("Update Comments Set Content=@Content where Id=@Id", con);
            cmd.Parameters.AddWithValue("@Content", entity.Content);
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            await con.CloseAsync();
            await con.DisposeAsync();
            return true;
        }

    }
}
