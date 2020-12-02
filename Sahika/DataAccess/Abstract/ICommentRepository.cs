using Sahika.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sahika.DataAccess.Abstract
{
    public interface ICommentRepository : IRepositoryBase<PostComment>
    {
         List<PostComment> GetCommentsByPost(int postId);
         List<PostComment> GetCommentsByUser(int userId);   
         PostComment AddCommentByUser(PostComment postComment);
         bool CommentExists(int userId, int postId);
        PostComment GetCommentOfPostByUserId(int postId, int userId);
         
    }
}
