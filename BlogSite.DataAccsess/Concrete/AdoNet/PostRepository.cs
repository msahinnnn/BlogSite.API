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


        public List<Post> GetAllPosts()
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            con.Open();
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
            con.Close();
            con.Dispose();
            return posts;
        }

        public Task<List<Post>> GetAllPostsAsync()
        {
            throw new NotImplementedException();
        }

        public Post GetPostById(Guid postId)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Users where Id=@Id", con);
            cmd.Parameters.AddWithValue("Id", postId);
            Post post = new Post();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            post.Id = postId;
            post.Title = ds.Tables[0].Rows[0]["Title"].ToString();
            post.Content = ds.Tables[0].Rows[0]["Content"].ToString();
            post.CreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
            post.UserId = Guid.Parse(ds.Tables[0].Rows[0]["UserId"].ToString());
            con.Close();
            con.Dispose();
            return post;
        }

        public Task<Post> GetPostByIdAsync(Guid postId)
        {
            throw new NotImplementedException();
        }

        public List<Post> GetPostsByUserId(Guid userId)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Posts where UserId='" + userId + '"', con);
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
            con.Close();
            con.Dispose();
            return posts;
        }

        public Task<List<Post>> GetPostsByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public bool CreatePost(Post post)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            SqlCommand cmd = new SqlCommand("Insert into Posts (Id, Title, Content, CreatedDate, UserId) values (@Id, @Title, @Content, @CreatedDate, @UserId)", con);
            cmd.Parameters.AddWithValue("@Id", post.Id);
            cmd.Parameters.AddWithValue("@Title", post.Title);
            cmd.Parameters.AddWithValue("@Content", post.Content);
            cmd.Parameters.AddWithValue("@CreatedDate", post.CreatedDate);
            cmd.Parameters.AddWithValue("@UserId", post.UserId);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            return true;
        }

        public Task<bool> CreatePostAsync(Post post)
        {
            throw new NotImplementedException();
        }

        public bool DeletePost(Guid postId)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            SqlCommand cmd = new SqlCommand("Delete from Posts where Id=@Id", con);
            cmd.Parameters.AddWithValue("Id", postId);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            return true;
        }

        public Task<bool> DeletePostAsync(Guid postId)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePost(Post post)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            SqlCommand cmd = new SqlCommand("Update Posts Set Title=@Title, Content=@Content where Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", post.Id);
            cmd.Parameters.AddWithValue("@Title", post.Title);
            cmd.Parameters.AddWithValue("@Content", post.Content);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            return true;
        }

        public Task<bool> UpdatePostAsync(Post post)
        {
            throw new NotImplementedException();
        }
    }
}
