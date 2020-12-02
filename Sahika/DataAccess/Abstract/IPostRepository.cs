using Sahika.Dtos;
using Sahika.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sahika.DataAccess.Abstract
{
    public interface IPostRepository : IRepositoryBase<Post>
    {
        List<Post> GetPostBySubCategory(int subCategoryId);
        Post  GetPostWithEverything(int postId);
        ICollection<Post> GetPostsByUser(int userId);
        Post GetPostByView();
        List<Post> GetPostsByCategory(int categoryId);
        List<Post> GetPostsByTwoDates(DateTime start, DateTime end);
        List<Post> GetPostsByDateCreated(int count);
        Post GetMostLikedPost();
        Post AddPostWithStatsAndCategory(Post post, int SubCategoryId);
        void DeletePostWithRelatedEntites(int postId);
        //Post GetMostPopularPostByCategory(int categoryId);
        List<Post> GetPostWithLikers();//henüaz yazılmadı
        bool PostExists(int postId);
        Post GetLastPostOfCategory(int categoryId);
        List<Post> GetAll();   
        Post GetLastPost();
        Post GetPostByUserId(int userId, int postId);
        Post GetPost(int postId);
       

        //getpostsbyuserlikes;

    }
}
