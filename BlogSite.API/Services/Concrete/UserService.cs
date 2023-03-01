using AutoMapper;
using BlogSite.API.Models;
using BlogSite.API.Services.Abstract;
using BlogSite.API.ViewModels.UserVMs;
using System.Data;
using System.Data.SqlClient;

namespace BlogSite.API.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private IMapper _mapper;

        public UserService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public void CreateUser(CreateUserVM createUserVM)
        {
            User user = _mapper.Map<User>(createUserVM);
            user.Id = Guid.NewGuid();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            SqlDataAdapter da = new SqlDataAdapter();
            con.Open();
            da.InsertCommand = new SqlCommand("Insert into Users values ('" + user.Id + "' , '" + user.Name + "' , '" + user.Surname + "' , '" + user.Email + "')", con);
            da.InsertCommand.ExecuteNonQuery();
            con.Close();
            con.Dispose();
        }

        public Task<bool> CreateUserAsync(CreateUserVM createUserVM)
        {
            throw new NotImplementedException();
        }

        public List<User> GetUsers()
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            SqlDataAdapter da = new SqlDataAdapter("Select * from Users", con);
            DataTable dt = new DataTable();
            List<User> users = new List<User>();
            da.Fill(dt);
            for(int i = 0; i < dt.Rows.Count; i++)
            { 
                User user= new User();
                user.Id = Guid.Parse(dt.Rows[i]["Id"].ToString());
                user.Name = dt.Rows[i]["Name"].ToString();
                user.Surname = dt.Rows[i]["Surname"].ToString();
                user.Email = dt.Rows[i]["Email"].ToString();
                users.Add(user);
            }
            return users;         
        }

        public Task<List<User>> GetUsersAsync()
        {
            throw new NotImplementedException();
        }
    }
}
