using Sahika.DataAccess.Abstract;
using Sahika.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Sahika.DataAccess.Concrete
{
    public class EfCommentRepository : EfRepositoryBase<PostComment, SahikaContext>, ICommentRepository
    {
        private SahikaContext _context;
        public EfCommentRepository(SahikaContext context) : base(context)
        {
            _context = context;
        }

        

        public List<PostComment> GetCommentsByPost(int postId)
        {
            var comments = _context.PostComments.Include(p => p.User).Where(p => p.PostId == postId).OrderByDescending(c=>c.PostCommentId).ToList();
             
            //var comments=   _context.PostComments.Where(p => p.PostId == postId).ToList();
            return comments;
        }

        public List<PostComment> GetCommentsByUser(int userId)
        {
            var comments = _context.PostComments.Where(p => p.User.IsDeleted == false && p.UserId == userId).ToList();
            return comments;
        }


        
        public PostComment AddCommentByUser(PostComment model)
        {
            //böyle bir post yok yahut böyle bir userId yok için sorgular eklenebilir fakat şişirmek istemedim
            //bool uniqueKeyControl =_context.PostComments.Count(c=>c.PostId ==model.PostId && c.UserId == model.UserId ) > 0;
            //if (uniqueKeyControl == true)
            //    return model;
           var modelToAdd= _context.PostComments.Add(model);
            _context.SaveChanges();
            return modelToAdd;

        }

        public bool CommentExists(int userId, int postId)
        {
            bool uniqueKeyControl = _context.PostComments.Count(c => c.PostId == postId && c.UserId == userId) > 0;
            return uniqueKeyControl;

        }

        public PostComment GetCommentOfPostByUserId(int postId, int userId)
        {
            var comment = _context.PostComments.Where(p => p.UserId == userId && p.PostId == postId).FirstOrDefault();
            return comment;
        }
    }
}