using BlogSite.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogSite.API.ApplicationContext
{
    public class BlogSiteDbContext:DbContext
    {
        //public DbSet<User> Users { get; set; }
        //public DbSet<Post> Posts { get; set; }
        //public DbSet<Comment> Comments { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=BlogSiteDB;Trusted_Connection=True;Encrypt=false;");
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //    modelBuilder.Entity<Post>()
        //        .HasOne(p => p.User)
        //        .WithMany(u => u.Posts)
        //        .HasForeignKey(up => up.UserId);

        //    modelBuilder.Entity<Comment>()
        //        .HasOne(c => c.Post)
        //        .WithMany(p => p.Comments)
        //        .HasForeignKey(pc => pc.PostId);


        //}


    }
}
