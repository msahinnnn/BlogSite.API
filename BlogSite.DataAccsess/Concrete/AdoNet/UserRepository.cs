using BlogSite.API.Models;
using BlogSite.API.ViewModels.UserVMs;
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
                user.Email = dt.Rows[i]["Email"].ToString();
                user.Role = dt.Rows[i]["Role"].ToString();
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
            user.Email = ds.Tables[0].Rows[0]["Email"].ToString();
            user.Role = ds.Tables[0].Rows[0]["Role"].ToString();
            await con.CloseAsync();
            await con.DisposeAsync();
            return user;
        }

        public async Task<User> CreateAsync(User entity)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            await con.OpenAsync();
            SqlCommand cmd = new SqlCommand("Insert into Users (Id, Email, Password, Role, Token, RefreshToken, RefreshTokenExpiryTime) values (@Id, @Email, @Password, @Role, @Token, @RefreshToken, @RefreshTokenExpiryTime)", con);
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@Email", entity.Email);
            cmd.Parameters.AddWithValue("@Password", entity.Password);
            cmd.Parameters.AddWithValue("@Role", entity.Role);
            cmd.Parameters.AddWithValue("@Token", entity.Token);
            cmd.Parameters.AddWithValue("@RefreshToken", entity.RefreshToken);
            cmd.Parameters.AddWithValue("@RefreshTokenExpiryTime", entity.RefreshTokenExpiryTime);
            await cmd.ExecuteNonQueryAsync();
            await con.CloseAsync();
            await con.DisposeAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            await con.OpenAsync();
            SqlCommand cmd = new SqlCommand("Delete from Users where Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            await cmd.ExecuteNonQueryAsync();
            await con.CloseAsync();
            await con.DisposeAsync();
            return true;
        }
        public async Task<bool> UpdateAsync(User entity)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            await con.OpenAsync();
            SqlCommand cmd = new SqlCommand("Update Users Set Password=@Password, Email=@Email, Token=@Token, RefreshToken=@RefreshToken, RefreshTokenExpiryTime=@RefreshTokenExpiryTime where Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@Email", entity.Email);
            cmd.Parameters.AddWithValue("@Password", entity.Password);
            cmd.Parameters.AddWithValue("@Token", entity.Token);
            cmd.Parameters.AddWithValue("@RefreshToken", entity.RefreshToken);
            cmd.Parameters.AddWithValue("@RefreshTokenExpiryTime", entity.RefreshTokenExpiryTime);
            await cmd.ExecuteNonQueryAsync();
            await con.CloseAsync();
            await con.DisposeAsync();
            return true;
        }

        public async Task<User> CheckUserEmailExistsAsync(string mail)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            await con.OpenAsync();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Users", con);
            DataTable dt = new DataTable();
            User user = new User();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                user.Id = Guid.Parse(dt.Rows[i]["Id"].ToString());
                user.Email = dt.Rows[i]["Email"].ToString();
                user.Role = dt.Rows[i]["Role"].ToString();
                user.Password = dt.Rows[i]["Password"].ToString();
                user.Token = dt.Rows[i]["Token"].ToString();
                user.RefreshToken= dt.Rows[i]["RefreshToken"].ToString();
                user.RefreshTokenExpiryTime = DateTime.Parse(dt.Rows[i]["RefreshTokenExpiryTime"].ToString());
                await con.CloseAsync();
                await con.DisposeAsync();
                return user;
            }
            await con.CloseAsync();
            await con.DisposeAsync();
            return null;

        }

        public bool CheckUserRefreshTokenExists(string refreshToken)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE RefreshToken=@RefreshToken", con);
            cmd.Parameters.AddWithValue("@RefreshToken", refreshToken);
            int exists = (int)cmd.ExecuteScalar();
            if (exists > 0)
            {
                con.Close();
                con.Dispose();
                return true;
            }
            con.Close();
            con.Dispose();
            return false;
        }
    }
}
