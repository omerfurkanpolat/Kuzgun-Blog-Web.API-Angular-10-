using Sahika.DataAccess.Abstract;
using Sahika.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sahika.DataAccess.Concrete
{
    public class EfFavouritePostRepository : EfRepositoryBase<FavouritePost, SahikaContext>, IFavouritePostRepository
    {
        private SahikaContext _context;
        public EfFavouritePostRepository(SahikaContext context) : base(context)
        {
            _context = context;
        }

        public FavouritePost GetByPostIdAndUserId(int postId, int userId)
        {
            var fav = _context.FavouritePosts.FirstOrDefault(f => f.PostId == postId && f.UserId == userId);
            return fav;
        }
   
        public FavouritePost AddFavByUser(FavouritePost model)
        {
            bool uniqueKeyControl = _context.FavouritePosts.Count(c => c.PostId == model.PostId && c.UserId == model.UserId) > 0;
            if (uniqueKeyControl == true)
                return model;
            var modelToAdd = _context.FavouritePosts.Add(model);

            return modelToAdd;
        }

       


    }
}