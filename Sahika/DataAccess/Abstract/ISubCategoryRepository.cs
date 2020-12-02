using Sahika.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sahika.DataAccess.Abstract
{
  public  interface ISubCategoryRepository : IRepositoryBase<SubCategory>
    {
        List<SubCategory> GetAllByCategoryId(int id);
        SubCategory UpdateSubCategoryWithCategoryId(SubCategory subCategory);
        void UpdateSubCategory(SubCategory subCategory);

        
    }
}
