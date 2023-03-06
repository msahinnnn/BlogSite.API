using BlogSite.API.Models;
using BlogSite.API.ViewModels.UserVMs;
using BlogSite.DataAccsess.Abstract;
using BlogSite.Entities.ViewModels.CommentVMs;
using BlogSite.Entities.ViewModels.UserVMs;
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
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
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
                user.Name = dt.Rows[i]["Name"].ToString();
                user.Surname = dt.Rows[i]["Surname"].ToString();
                user.Email = dt.Rows[i]["Email"].ToString();
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
            cmd.Parameters.AddWithValue("Id", id);
            User user = new User();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            user.Id = id;
            user.Name = ds.Tables[0].Rows[0]["Name"].ToString();
            user.Surname = ds.Tables[0].Rows[0]["Surname"].ToString();
            user.Email = ds.Tables[0].Rows[0]["Email"].ToString();
            await con.CloseAsync();
            await con.DisposeAsync();
            return user;
        }

        public async Task<bool> CreateAsync(User entity)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            await con.OpenAsync();
            SqlCommand cmd = new SqlCommand("Insert into Users (Id, Name, Surname, Email) values (@Id, @Name, @Surname, @Email)", con);
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@Surname", entity.Surname);
            cmd.Parameters.AddWithValue("@Email", entity.Email);
            await cmd.ExecuteNonQueryAsync();
            await con.CloseAsync();
            await con.DisposeAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            SqlCommand cmd = new SqlCommand("Delete from Users where Id=@Id", con);
            cmd.Parameters.AddWithValue("Id", id);
            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            await con.CloseAsync();
            await con.DisposeAsync();
            return true;
        }
        public async Task<bool> UpdateAsync(User entity)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            SqlCommand cmd = new SqlCommand("Update Users Set Name=@Name, Surname=@Surname, Email=@Email where Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@Surname", entity.Surname);
            cmd.Parameters.AddWithValue("@Email", entity.Email);
            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            await con.CloseAsync();
            await con.DisposeAsync();
            return true;
        }

        public async Task<bool> CheckUserEmailExistsAsync(string mail)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            await con.OpenAsync();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Email=@Email", con);
            cmd.Parameters.AddWithValue("@Email", mail);
            int exists =  (int)cmd.ExecuteScalar();
            if (exists > 0 )
            {
                await con.CloseAsync();
                await con.DisposeAsync();
                return true;              
            }
            await con.CloseAsync();
            await con.DisposeAsync();
            return false;

        }

        //public bool CheckUserEmailExists(string mail)
        //{
        //    SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
        //    con.Open();
        //    SqlCommand cmd = new SqlCommand("Select * from Users where Email=@Email", con);
        //    cmd.Parameters.AddWithValue("Email", mail);
        //    User user = new User();
        //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    adp.Fill(ds);
        //    user.Id = Guid.Parse(ds.Tables[0].Rows[0]["Id"].ToString()); ;
        //    user.Name = ds.Tables[0].Rows[0]["Name"].ToString();
        //    user.Surname = ds.Tables[0].Rows[0]["Surname"].ToString();
        //    user.Email = ds.Tables[0].Rows[0]["Email"].ToString();
        //    if(user == null)
        //    {
        //        con.Close();
        //        con.Dispose();
        //        return false;  
        //    }
        //    con.Close();
        //    con.Dispose();
        //    return true;
        //}

        //public async Task<bool> CheckUserEmailExistsAsync(string mail)
        //{
        //    SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
        //    await con.OpenAsync();
        //    SqlCommand cmd = new SqlCommand("Select * from Users where Email=@Email", con);
        //    cmd.Parameters.AddWithValue("Email", mail);
        //    User user = new User();
        //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    adp.Fill(ds);
        //    user.Id = Guid.Parse(ds.Tables[0].Rows[0]["Id"].ToString()); ;
        //    user.Name = ds.Tables[0].Rows[0]["Name"].ToString();
        //    user.Surname = ds.Tables[0].Rows[0]["Surname"].ToString();
        //    user.Email = ds.Tables[0].Rows[0]["Email"].ToString();
        //    if (user == null)
        //    {
        //        await con.CloseAsync();
        //        await con.DisposeAsync();
        //        return false;
        //    }
        //    await con.CloseAsync();
        //    await con.DisposeAsync();
        //    return true;
        //}

        //public List<User> GetAllUsers()
        //{
        //    SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
        //    con.Open();
        //    SqlDataAdapter da = new SqlDataAdapter("Select * from Users", con);
        //    DataTable dt = new DataTable();
        //    List<User> users = new List<User>();
        //    da.Fill(dt);
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        User user = new User();
        //        user.Id = Guid.Parse(dt.Rows[i]["Id"].ToString());
        //        user.Name = dt.Rows[i]["Name"].ToString();
        //        user.Surname = dt.Rows[i]["Surname"].ToString();
        //        user.Email = dt.Rows[i]["Email"].ToString();
        //        users.Add(user);
        //    }
        //    con.Close();
        //    con.Dispose();
        //    return users;
        //}

        //public async Task<List<User>> GetAllUsersAsync()
        //{
        //    SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
        //    await con.OpenAsync();
        //    SqlDataAdapter da = new SqlDataAdapter("Select * from Users", con);
        //    DataTable dt = new DataTable();
        //    List<User> users = new List<User>();
        //    da.Fill(dt);
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        User user = new User();
        //        user.Id = Guid.Parse(dt.Rows[i]["Id"].ToString());
        //        user.Name = dt.Rows[i]["Name"].ToString();
        //        user.Surname = dt.Rows[i]["Surname"].ToString();
        //        user.Email = dt.Rows[i]["Email"].ToString();
        //        users.Add(user);
        //    }
        //    await con.CloseAsync();
        //    await con.DisposeAsync();
        //    return users;
        //}

        //public User GetUserById(Guid userId)
        //{
        //    SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
        //    con.Open();
        //    SqlCommand cmd = new SqlCommand("Select * from Users where Id=@Id", con);
        //    cmd.Parameters.AddWithValue("Id", userId);
        //    User user = new User();
        //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    adp.Fill(ds);
        //    user.Id = userId;
        //    user.Name = ds.Tables[0].Rows[0]["Name"].ToString();
        //    user.Surname = ds.Tables[0].Rows[0]["Surname"].ToString();
        //    user.Email = ds.Tables[0].Rows[0]["Email"].ToString();
        //    con.Close();
        //    con.Dispose();
        //    return user;
        //}

        //public async Task<User> GetUserByIdAsync(Guid userId)
        //{
        //    SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
        //    await con.OpenAsync();
        //    SqlCommand cmd = new SqlCommand("Select * from Users where Id=@Id", con);
        //    cmd.Parameters.AddWithValue("Id", userId);
        //    User user = new User();
        //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    adp.Fill(ds);
        //    user.Id = userId;
        //    user.Name = ds.Tables[0].Rows[0]["Name"].ToString();
        //    user.Surname = ds.Tables[0].Rows[0]["Surname"].ToString();
        //    user.Email = ds.Tables[0].Rows[0]["Email"].ToString();
        //    await con.CloseAsync();
        //    await con.DisposeAsync();
        //    return user;
        //}

        //public bool CreateUser(User user)
        //{
        //    SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
        //    con.Open();
        //    SqlCommand cmd = new SqlCommand("Insert into Users (Id, Name, Surname, Email) values (@Id, @Name, @Surname, @Email)", con);
        //    cmd.Parameters.AddWithValue("@Id", user.Id);
        //    cmd.Parameters.AddWithValue("@Name", user.Name);
        //    cmd.Parameters.AddWithValue("@Surname", user.Surname);
        //    cmd.Parameters.AddWithValue("@Email", user.Email);
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //    con.Dispose();         
        //    return true;
        //}

        //public async Task<bool> CreateUserAsync(User user)
        //{
        //    SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
        //    await con.OpenAsync();
        //    SqlCommand cmd = new SqlCommand("Insert into Users (Id, Name, Surname, Email) values (@Id, @Name, @Surname, @Email)", con);
        //    cmd.Parameters.AddWithValue("@Id", user.Id);
        //    cmd.Parameters.AddWithValue("@Name", user.Name);
        //    cmd.Parameters.AddWithValue("@Surname", user.Surname);
        //    cmd.Parameters.AddWithValue("@Email", user.Email);
        //    await cmd.ExecuteNonQueryAsync();
        //    await con.CloseAsync();
        //    await con.DisposeAsync();
        //    return true;
        //}

        //public bool DeleteUser(Guid userId)
        //{
        //    SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
        //    SqlCommand cmd = new SqlCommand("Delete from Users where Id=@Id", con);
        //    cmd.Parameters.AddWithValue("Id", userId);
        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //    con.Dispose();
        //    return true;
        //}

        //public async Task<bool> DeleteUserAsync(Guid userId)
        //{
        //    SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
        //    SqlCommand cmd = new SqlCommand("Delete from Users where Id=@Id", con);
        //    cmd.Parameters.AddWithValue("Id", userId);
        //    await con.OpenAsync();
        //    await cmd.ExecuteNonQueryAsync();
        //    await con.CloseAsync();
        //    await con.DisposeAsync();
        //    return true;
        //}


        //public bool UpdateUser(User user)
        //{
        //    SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
        //    SqlCommand cmd = new SqlCommand("Update Users Set Name=@Name, Surname=@Surname, Email=@Email where Id=@Id", con);
        //    cmd.Parameters.AddWithValue("@Id", user.Id);
        //    cmd.Parameters.AddWithValue("@Name",  user.Name);
        //    cmd.Parameters.AddWithValue("@Surname", user.Surname);
        //    cmd.Parameters.AddWithValue("@Email", user.Email);
        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //    con.Dispose();
        //    return true;
        //}

        //public async Task<bool> UpdateUserAsync(User user)
        //{
        //    SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
        //    SqlCommand cmd = new SqlCommand("Update Users Set Name=@Name, Surname=@Surname, Email=@Email where Id=@Id", con);
        //    cmd.Parameters.AddWithValue("@Id", user.Id);
        //    cmd.Parameters.AddWithValue("@Name", user.Name);
        //    cmd.Parameters.AddWithValue("@Surname", user.Surname);
        //    cmd.Parameters.AddWithValue("@Email", user.Email);
        //    await con.OpenAsync();
        //    await cmd.ExecuteNonQueryAsync();
        //    await con.CloseAsync();
        //    await con.DisposeAsync();
        //    return true;
        //}
    }
}
