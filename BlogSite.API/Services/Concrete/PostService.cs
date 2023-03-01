using AutoMapper;
using BlogSite.API.Models;
using BlogSite.API.Services.Abstract;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.API.ViewModels.UserVMs;
using System.Data;
using System.Data.SqlClient;

namespace BlogSite.API.Services.Concrete
{
    public class PostService : IPostService
    {
        private readonly IConfiguration _configuration;
        private IMapper _mapper;

        public PostService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public void CreatePost(CreatePostVM createPostVM, Guid userId)
        {
            Post post = _mapper.Map<Post>(createPostVM);
            post.Id = Guid.NewGuid();
            post.CreatedDate = DateTime.UtcNow;
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            SqlDataAdapter da = new SqlDataAdapter();
            con.Open();
            da.InsertCommand = new SqlCommand("Insert into Posts values ('" + post.Id + "' , '" + post.Title + "' , '" + post.Content + "' , '" + post.CreatedDate + "' , '" + userId + "')", con);
            da.InsertCommand.ExecuteNonQuery();
            con.Close();
            con.Dispose();
        }

        public Task<bool> CreatePostAsync(CreatePostVM createPostVM, Guid userId)
        {
            throw new NotImplementedException();
        }

        public List<Post> GetPosts( )
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            SqlDataAdapter da = new SqlDataAdapter("Select * from Posts order by CreatedDate desc", con);
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
                posts.Add(post);
            }
            return posts;
        }

        public Task<List<Post>> GetPostsAsync( )
        {
            throw new NotImplementedException();
        }
    }
}
