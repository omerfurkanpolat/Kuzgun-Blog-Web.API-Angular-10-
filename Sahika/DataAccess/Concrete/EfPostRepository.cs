using Sahika.DataAccess.Abstract;
using Sahika.Dtos;
using Sahika.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Http.Results;
using System.Xml;

namespace Sahika.DataAccess.Concrete
{
    public class EfPostRepository : EfRepositoryBase<Post, SahikaContext>, IPostRepository
    {

        private SahikaContext _context;
        public EfPostRepository(SahikaContext context) : base(context)
        {
            _context = context;
        }

        public List<Post> GetPostBySubCategory(int subCategoryId)
        {
            //var posts = _context.Posts.Where(p => p.SubCategoryPosts.Any(s => s.SubCategories.SubCategoryId ==subCategoryId)).ToList();
            var posts = _context.Posts.Include(p => p.User).Include(p => p.Category)
                .Include(p => p.SubCategory).Where(p => p.SubCategoryId == subCategoryId).OrderByDescending(p=>p.PostId).ToList();
            return posts;
        }



        public ICollection<Post> GetPostsByUser(int userId)
        {
            var posts = _context.Posts.Include(p=>p.User).Include(p=>p.SubCategory)
                .Include(p=>p.Category).Where(p => p.User.IsDeleted == false && p.UserId == userId)
                .OrderByDescending(p => p.PostId).ToList();
            return posts;
        }


        public Post GetPostWithEverything(int postId)
        {

            //YORUMLAR AYRICA GELECEK 
            var post = _context.Posts.Include(p => p.PostStat)
                                      .Include(p => p.User).Include(p => p.Category)
                                      .Include(p => p.SubCategory)
                                      .Where(p => p.IsPublished == true)
                                      .FirstOrDefault(p => p.PostId == postId);
            return post;
        }


        public Post GetPostByView()
        {
            var post = (from p in _context.Posts
                        from ps in _context.PostStats
                        where p.PostId == ps.PostStatId && p.IsPublished == true
                        orderby ps.Views descending
                        select p).FirstOrDefault();

            return post;

        }

        public List<Post> GetPostsByCategory(int categoryId)
        {
            var posts = _context.Posts.Include(p => p.User)
                .Include(p => p.Category).Include(p => p.SubCategory)
                .Where(p => p.CategoryId == categoryId).OrderByDescending(p=>p.PostId).ToList();   
            return posts;
        }

        public List<Post> GetPostsByTwoDates(DateTime start, DateTime end)
        {
            var posts = _context.Posts.Where(p => p.DateCreated >= start && p.DateCreated <= end && p.IsPublished == true).Select(x =>
             new Post
             {
                 PostId = x.PostId,
                 Title = x.Title,
                 Body = x.Body,
                 DateCreated = x.DateCreated,
                 ImageUrl = x.ImageUrl,
                 User = x.User,


             }).ToList();
            return posts;
        }
        public List<Post> GetPostsByDateCreated(int count)
        {

            var posts = _context.Posts.Where(p => p.IsPublished == true).OrderByDescending(p => p.DateCreated).Take(count).ToList();
            return posts;

        }

        public Post GetMostLikedPost()
        {
            var post = _context.Posts.Where(p => p.IsPublished == true).OrderByDescending(p => p.PostStat.Claps).FirstOrDefault();
            return post;
        }

        //public Post GetMostPopularPostByCategory(int categoryId)
        //{
        //    //var post = (from p in _context.Posts
        //    //            join s in _context.SubCategoryPosts
        //    //            on p.PostId equals s.PostId
        //    //            join c in _context.SubCategories
        //    //            on s.SubCategoryId equals c.SubCategoryId
        //    //            join m in _context.Categories
        //    //            on c.CategoryId equals m.CategoryId
        //    //            where c.CategoryId == categoryId && p.IsPublished == true
        //    //            select p).OrderByDescending(p => p.PostStat.Views).First();

        //    //return post;
        //}


        public Post AddPostWithStatsAndCategory(Post post, int subCategoryId)
        {
            _context.Posts.Add(post);

            PostStat postStat = new PostStat();
            postStat.PostStatId = post.PostId;
            postStat.Views = 0;
            postStat.Claps = 0;
            _context.PostStats.Add(postStat);

           



            _context.SaveChanges();

            return post;
        }

        public void DeletePostWithRelatedEntites(int postId)
        {
            var post = _context.Posts.Include(p => p.PostStat).FirstOrDefault(p => p.PostId == postId);
            var postFavs = _context.FavouritePosts.Where(f => f.PostId == post.PostId).ToList();
            var postComments = _context.PostComments.Where(pc => pc.PostId == post.PostId).ToList();

            _context.FavouritePosts.RemoveRange(postFavs);
            _context.PostComments.RemoveRange(postComments);
            _context.Posts.Remove(post);
            _context.SaveChanges();

        }



        public bool PostExists(int postId)
        {
            var post = _context.Posts.FirstOrDefault(p => p.PostId == postId);
            if (post == null)
                return true;
            return false;
        }

        public List<Post> GetPostWithLikers()
        {
            throw new NotImplementedException();
        }

        public Post GetLastPost()
        {
            var post = _context.Posts.Include(p => p.User).Include(p => p.Category).Include(p => p.SubCategory).OrderByDescending(p => p.PostId).FirstOrDefault();
            return post;
        }

        public List<Post> GetAll()
        {
            return _context.Posts.Include(p => p.User).Include(p => p.Category).Include(p => p.SubCategory).OrderByDescending(x => x.PostId).ToList();
        }

        public Post GetLastPostOfCategory(int categoryId)
        {
            var post = _context.Posts.Include(p => p.User).Include(p => p.Category)
                .Include(p => p.SubCategory).OrderByDescending(p => p.PostId)
                .FirstOrDefault(p => p.CategoryId == categoryId);
            return post;

        }


        public Post GetPostByUserId(int userId, int postId)
        {
            var post = _context.Posts.Include(p => p.User).FirstOrDefault(p => p.UserId == userId && p.PostId == postId);
            return post;
        }

        public Post GetPost(int postId)
        {
            var post = _context.Posts.Include(p => p.User).Include(p => p.Category).Include(p => p.SubCategory).FirstOrDefault(p => p.PostId == postId);
            return post;
        }

       
    }
}