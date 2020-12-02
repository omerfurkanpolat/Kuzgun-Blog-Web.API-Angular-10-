namespace Sahika.Migrations
{
    using Sahika.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using static Sahika.Models.IdentityModels;

    internal sealed class Configuration : DbMigrationsConfiguration<Sahika.DataAccess.Concrete.SahikaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Sahika.DataAccess.Concrete.SahikaContext context)
        {

            List<ApplicationUser> Users = new List<ApplicationUser>();
            for (int i = 4; i < 20; i++)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = FakeData.NameData.GetFullName();
                user.Email = FakeData.NetworkData.GetEmail();
                user.EmailConfirmed = true;
                user.PhoneNumber = FakeData.PhoneNumberData.GetPhoneNumber();
                user.PhoneNumberConfirmed = true;
                Users.Add(user);
                context.Users.Add(user);
            }

            context.SaveChanges();
            List<Post> Posts = new List<Post>();
            for (int i = 0; i < 15; i++)
            {
                Post post = new Post();
                post.Title = FakeData.TextData.GetSentence();
                post.Body = FakeData.TextData.GetSentence();
                post.DateCreated = FakeData.DateTimeData.GetDatetime();
                post.UserId = i + 1;
                Posts.Add(post);
                context.Posts.Add(post);

            }
            context.SaveChanges();

            List<Category> Categories = new List<Category>();
            for (int i = 0; i < 5; i++)
            {
                Category category = new Category();
                category.CategoryName = FakeData.TextData.GetSentence();
              
                category.DateCreated = FakeData.DateTimeData.GetDatetime();
                Categories.Add(category);
                context.Categories.Add(category);
            }
            context.SaveChanges();


            List<SubCategory> SubCategories = new List<SubCategory>();
            for (int i = 0; i < 5; i++)
            {
                SubCategory subCategory = new SubCategory();
                subCategory.SubCategoryName = FakeData.TextData.GetSentence();
                subCategory.CategoryId = i + 1;
                SubCategories.Add(subCategory);
                context.SubCategories.Add(subCategory);
            }
            context.SaveChanges();

            

            List<PostComment> PostComments = new List<PostComment>();
            for (int i = 1; i < 5; i++)
            {

                for (int j = 6; j < 9; j++)
                {
                    PostComment postComment = new PostComment();
                    postComment.PostId = j;
                    postComment.UserId = i;
                    postComment.Comment = FakeData.TextData.GetSentence();
                    PostComments.Add(postComment);
                    context.PostComments.Add(postComment);
                }

            }
            context.SaveChanges();

            List<FavouritePost> FavouritePosts = new List<FavouritePost>();
            for (int i = 1; i < 5; i++)
            {
                FavouritePost favouritePost = new FavouritePost();
                favouritePost.UserId = 1;
                favouritePost.PostId = i + 2;
                FavouritePosts.Add(favouritePost);
                context.FavouritePosts.Add(favouritePost);
            }
            context.SaveChanges();


            List<FavouritePost> FavouritePosts2 = new List<FavouritePost>();
            for (int i = 5; i < 10; i++)
            {
                FavouritePost favouritePost = new FavouritePost();
                favouritePost.UserId = 2;
                favouritePost.PostId = i;
                FavouritePosts2.Add(favouritePost);
                context.FavouritePosts.Add(favouritePost);
            }
            context.SaveChanges();

            List<FavouritePost> FavouritePosts3 = new List<FavouritePost>();
            for (int i = 10; i < 15; i++)
            {
                FavouritePost favouritePost = new FavouritePost();
                favouritePost.UserId = 3;
                favouritePost.PostId = i;
                FavouritePosts3.Add(favouritePost);
                context.FavouritePosts.Add(favouritePost);
            }
            context.SaveChanges();
            List<PostStat> PostStats = new List<PostStat>();
            for (int i = 0; i < 10; i++)
            {
                PostStat postStat = new PostStat();
                postStat.PostStatId = i + 1;
                postStat.Views = i + 25;
                postStat.Claps = i + 35;
                PostStats.Add(postStat);
                context.PostStats.Add(postStat);

            }
            context.SaveChanges();

        }
    }
}
