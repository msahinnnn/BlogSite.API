using AutoMapper;
using BlogSite.API.Models;
using BlogSite.API.Services.Abstract;
using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.API.ViewModels.UserVMs;
using System.Data;
using System.Data.SqlClient;

namespace BlogSite.API.Services.Concrete
{
    public class CommentService : ICommentService
    {
        private readonly IConfiguration _configuration;
        private IMapper _mapper;

        public CommentService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public void CreateComment(CreateCommentVM createCommentVM, Guid postId)
        {
            Comment comment = _mapper.Map<Comment>(createCommentVM);
            comment.Id = Guid.NewGuid();
            comment.CreateTime = DateTime.UtcNow;
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            SqlDataAdapter da = new SqlDataAdapter();
            con.Open();
            da.InsertCommand = new SqlCommand("Insert into Comments values ('" + comment.Id + "' , '" + comment.Content + "' , '" + comment.CreateTime + "' , '"  + postId + "')", con);
            da.InsertCommand.ExecuteNonQuery();
            con.Close();
            con.Dispose();
        }

        public Task<bool> CreateCommentAsync(CreateCommentVM createCommentVM, Guid postId)
        {
            throw new NotImplementedException();
        }

        public List<Comment> GetComments()
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
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
            return comments;
        }

        public Task<List<Comment>> GetCommentsAsync()
        {
            throw new NotImplementedException();
        }
    }
}

