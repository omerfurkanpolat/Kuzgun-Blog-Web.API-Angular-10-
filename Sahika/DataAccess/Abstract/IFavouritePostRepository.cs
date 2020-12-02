using Sahika.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sahika.DataAccess.Abstract
{
 public  interface IFavouritePostRepository : IRepositoryBase<FavouritePost>
    {
      
        FavouritePost GetByPostIdAndUserId(int postId,int userId);
        FavouritePost AddFavByUser(FavouritePost favouritePost);
      
    }
}
