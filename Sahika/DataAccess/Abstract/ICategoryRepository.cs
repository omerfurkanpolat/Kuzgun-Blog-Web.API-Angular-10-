using Sahika.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sahika.DataAccess.Abstract
{
    public interface ICategoryRepository :IRepositoryBase<Category>
    {
        //ADD_UPDATE_DELETE'İ BASEREPO'DAN ALACAK 
        List<Category> GetAll();
        
        Category GetCategoryWithSubs(int categoryId);
        Category GetCategoryWithPosts(int categoryId);
        bool CategoryExists(int categoryId);
        List<int> GetCategoryIds();
      

        
    }
}
