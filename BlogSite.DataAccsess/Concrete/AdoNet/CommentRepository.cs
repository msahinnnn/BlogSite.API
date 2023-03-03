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

        public List<Comment> GetAllComments()
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            con.Open();
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
            con.Close();
            con.Dispose();
            return comments;
        }

        public Task<List<Comment>> GetAllCommentsAsync()
        {
            throw new NotImplementedException();
        }

        public Comment GetCommentById(Guid commentId)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Comments where Id=@Id", con);
            cmd.Parameters.AddWithValue("Id", commentId);
            Comment comment = new Comment();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            comment.Id = commentId;
            comment.PostId = Guid.Parse(ds.Tables[0].Rows[0]["PostId"].ToString());
            comment.Content = ds.Tables[0].Rows[0]["Content"].ToString();
            comment.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
            //cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            return comment;
        }

        public Task<Comment> GetCommentByIdAsync(Guid commentId)
        {
            throw new NotImplementedException();
        }

        public List<Comment> GetCommentsByPostId(Guid postId)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Comments where PostId='" + postId + '"', con);
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
            con.Close();
            con.Dispose();
            return comments;
        }

        public Task<List<Comment>> GetCommentsByPostIdAsync(Guid postId)
        {
            throw new NotImplementedException();
        }

        public bool CreateComment(Comment comment)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            SqlCommand cmd = new SqlCommand("Insert into Comments (Id, Content, CreateTime, PostId) values (@Id, @Content, @CreateTime, @PostId)", con);
            cmd.Parameters.AddWithValue("@Id", comment.Id);
            cmd.Parameters.AddWithValue("@Content", comment.Content);
            cmd.Parameters.AddWithValue("@CreateTime", comment.CreateTime);
            cmd.Parameters.AddWithValue("@PostId", comment.PostId);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            return true;
        }

        public Task<bool> CreateCommentAsync(Comment comment)
        {
            throw new NotImplementedException();
        }

        public bool DeleteComment(Guid commentId)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            SqlCommand cmd = new SqlCommand("Delete from Comments where Id=@Id", con);
            cmd.Parameters.AddWithValue("Id", commentId);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            return true;
        }

        public Task<bool> DeleteCommentAsync(Guid commentId)
        {
            throw new NotImplementedException();
        }

        

        public bool UpdateComment(Comment comment)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            SqlCommand cmd = new SqlCommand("Update Comments Set Content=@Content where Id=@Id", con);
            cmd.Parameters.AddWithValue("@Content", comment.Content);
            cmd.Parameters.AddWithValue("@Id", comment.Id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            return true;
        }

        public Task<bool> UpdateCommentAsync(Comment comment)
        {
            throw new NotImplementedException();
        }
    }
}
