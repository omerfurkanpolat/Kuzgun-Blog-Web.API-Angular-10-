using Microsoft.AspNet.Identity.EntityFramework;
using Sahika.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using static Sahika.App_Start.IdentityConfig;
using static Sahika.Models.IdentityModels;

namespace Sahika.DataAccess.Concrete
{
    public class SahikaContext : IdentityDbContext<ApplicationUser, ApplicationRole, int,
       ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public SahikaContext() : base("Sahika")
        {

        }
        static SahikaContext()
        {
            Database.SetInitializer<SahikaContext>(new ApplicationDbInitializer());
        }

        public static SahikaContext Create()
        {
            return new SahikaContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<PostComment>().HasOptional(p=>p.Post).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<FavouritePost>().HasOptional(f => f.Post).WithMany().WillCascadeOnDelete(false);
            //   modelBuilder.Entity<Post>().HasOptional(p => p.User).WithMany().WillCascadeOnDelete(false);
            //modelBuilder.Entity<Category>().HasOptional(p => p.Posts).WithMany().WillCascadeOnDelete(false);
            //modelBuilder.Entity<SubCategory>().HasOptional(p => p.Posts).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Post>().HasOptional(p => p.Category).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Post>().HasOptional(p => p.SubCategory).WithMany().WillCascadeOnDelete(false);


            //postcomments'in unique key builder'ı
            modelBuilder.Entity<PostComment>().HasIndex(p => new { p.PostId, p.UserId }).IsUnique();
            
            modelBuilder.Entity<FavouritePost>().HasIndex(f => new {f.PostId,f.UserId }).IsUnique();


        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PostStat> PostStats { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        
        public DbSet<FavouritePost> FavouritePosts { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<Message> Messages { get; set; }

    }
}