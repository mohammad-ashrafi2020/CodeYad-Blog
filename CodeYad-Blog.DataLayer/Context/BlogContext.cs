using CodeYad_Blog.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CodeYad_Blog.DataLayer.Context
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserFollower> UserFollowers { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostTag> PostTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<Post>().OwnsOne(b => b.SeoData);
            modelBuilder.Entity<Category>().OwnsOne(b => b.SeoData);
            base.OnModelCreating(modelBuilder);
        }
    }
}
