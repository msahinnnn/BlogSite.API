using BlogSite.API.Models;
using BlogSite.API.ViewModels.UserVMs;
using BlogSite.Core.Entities.Concrete;
using BlogSite.DataAccsess.Abstract;
using BlogSite.DataAccsess.EntitiyFramework.ApplicationContext;
using BlogSite.Entities.ViewModels.CommentVMs;
using BlogSite.Entities.ViewModels.UserVMs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private BlogSiteDbContext _dbContext;
        public UserRepository(IConfiguration configuration, BlogSiteDbContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }


        public async Task<List<User>> GetAllAsync()
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            await con.OpenAsync();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Users", con);
            DataTable dt = new DataTable();
            List<User> users = new List<User>();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                User user = new User();
                user.Id = Guid.Parse(dt.Rows[i]["Id"].ToString());
                user.FirstName = dt.Rows[i]["FirstName"].ToString();
                user.LastName = dt.Rows[i]["LastName"].ToString();
                user.Email = dt.Rows[i]["Email"].ToString();
                user.Status = Convert.ToBoolean(dt.Rows[i]["Status"].ToString());
                users.Add(user);
            }
            await con.CloseAsync();
            await con.DisposeAsync();
            return users;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            await con.OpenAsync();
            SqlCommand cmd = new SqlCommand("Select * from Users where Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            User user = new User();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            user.Id = id;
            user.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
            user.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
            user.Email = ds.Tables[0].Rows[0]["Email"].ToString();
            user.Status = Convert.ToBoolean(ds.Tables[0].Rows[0]["Status"].ToString());
            await con.CloseAsync();
            await con.DisposeAsync();
            return user;
        }

        public async Task<User> CreateAsync(User entity)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            await con.OpenAsync();
            SqlCommand cmd = new SqlCommand("Insert into Users (Id, FirstName, LastName, Email, PasswordSalt, PasswordHash, Status) values (@Id, @FirstName, @LastName, @Email, @PasswordSalt, @PasswordHash, @Status)", con);
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@FirstName", entity.FirstName);
            cmd.Parameters.AddWithValue("@LastName", entity.LastName);
            cmd.Parameters.AddWithValue("@Email", entity.Email);
            cmd.Parameters.AddWithValue("@PasswordSalt", entity.PasswordSalt);
            cmd.Parameters.AddWithValue("@PasswordHash", entity.PasswordHash);
            cmd.Parameters.AddWithValue("@Status", entity.Status);
            await cmd.ExecuteNonQueryAsync();
            await con.CloseAsync();
            await con.DisposeAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            SqlCommand cmd = new SqlCommand("Delete from Users where Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            await con.CloseAsync();
            await con.DisposeAsync();
            return true;
        }
        public async Task<bool> UpdateAsync(User entity)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            SqlCommand cmd = new SqlCommand("Update Users Set Name=@FirstName, Surname=@LastName, Email=@Email where Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@FirstName", entity.FirstName);
            cmd.Parameters.AddWithValue("@LastName", entity.LastName);
            cmd.Parameters.AddWithValue("@Email", entity.Email);
            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            await con.CloseAsync();
            await con.DisposeAsync();
            return true;
        }

        public async Task<User> CheckUserEmailExistsAsync(string mail)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            await con.OpenAsync();
            SqlCommand cmd = new SqlCommand("Select * from Users where Email=@Email", con);
            cmd.Parameters.AddWithValue("@Email", mail);
            SqlDataReader reader = cmd.ExecuteReader();
            User user = new User();
            while (reader.Read())
            {
                user.Id = Guid.Parse(reader["Id"].ToString());
                user.FirstName = reader["FirstName"].ToString();
                user.LastName = reader["LastName"].ToString();
                user.Email = mail;
                user.Status = Convert.ToBoolean(reader["Status"].ToString());
                user.PasswordHash = Encoding.UTF8.GetBytes(reader["PasswordHash"].ToString());
                user.PasswordSalt = Encoding.UTF8.GetBytes(reader["PasswordSalt"].ToString());
            }
            await con.CloseAsync();
            await con.DisposeAsync();
            return user;

            //SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            //await con.OpenAsync();
            //SqlCommand cmd = new SqlCommand("Select * from Users where Email=@Email", con);
            //cmd.Parameters.AddWithValue("Email", mail);
            //User user = new User();
            //SqlDataAdapter adp = new SqlDataAdapter(cmd);
            //DataSet ds = new DataSet();
            //adp.Fill(ds);
            //user.Id = Guid.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
            //user.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
            //user.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
            //user.PasswordHash = Encoding.UTF8.GetBytes(ds.Tables[0].Rows[0]["PasswordHash"].ToString());
            //user.PasswordSalt = Encoding.UTF8.GetBytes(ds.Tables[0].Rows[0]["PasswordSalt"].ToString());
            //user.Email = mail;
            //user.Status = Convert.ToBoolean(ds.Tables[0].Rows[0]["Status"].ToString());
            //await con.CloseAsync();
            //await con.DisposeAsync();
            //return user;

            //SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            //await con.OpenAsync();
            //SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Email=@Email", con);
            //cmd.Parameters.AddWithValue("@Email", mail);
            //int exists = (int)cmd.ExecuteScalar();
            //if (exists > 0)
            //{
            //    await con.CloseAsync();
            //    await con.DisposeAsync();
            //    return true;
            //}
            //await con.CloseAsync();
            //await con.DisposeAsync();
            //return false;

        }

        public async Task<List<OperationClaim>> GetClaims(User user)
        {
            //using (BlogSiteDbContext context = new BlogSiteDbContext())
            //{
                string id = user.Id.ToString();
                var result = from operationClaim in _dbContext.OperationClaims
                             join userOperationClaim in _dbContext.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Name = operationClaim.Name };
                return await result.ToListAsync();

            //}
        }

  
    }
}
