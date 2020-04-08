using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        //tables
        public DbSet<Tree> Trees { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Leaf> Leaves { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<UserPhoto> UserPhotos { get; set; }
        public DbSet<UserRoot> UserRoots { get; set; } //jxn table
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //set M:M relationship between a user and a tree
            builder.Entity<UserRoot>(b => {
               b.HasKey(ur => new{ur.AppUserId, ur.TreeId});

               //on delete behavior is CASCADE by default
               //that is if an AppUser or Tree is deleted, so does the related UserRoot
               b.HasOne(ur => ur.AppUser)
                .WithMany(au => au.UserRoots)
                .HasForeignKey(ur => ur.AppUserId);
                
               b.HasOne(ur => ur.Tree)
                .WithMany(au => au.UserRoots)
                .HasForeignKey(ur => ur.TreeId);
            });

        }
    }
}