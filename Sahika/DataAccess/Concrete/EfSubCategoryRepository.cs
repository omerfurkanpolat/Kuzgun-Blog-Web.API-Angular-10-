using Sahika.DataAccess.Abstract;
using Sahika.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Sahika.DataAccess.Concrete
{
    public class EfSubCategoryRepository : EfRepositoryBase<SubCategory, SahikaContext>, ISubCategoryRepository
    {
        private SahikaContext _context;
        public EfSubCategoryRepository(SahikaContext context) : base(context)
        {
            _context = context;
        }

        public List<SubCategory> GetAllByCategoryId(int id)
        {
            return _context.SubCategories.Where(s => s.CategoryId == id).ToList();
        }

        public void UpdateSubCategory(SubCategory subCategory)
        {
            var subCategor = _context.SubCategories.FirstOrDefault(s => s.SubCategoryId == subCategory.SubCategoryId);
            subCategor.SubCategoryName = subCategory.SubCategoryName;
            _context.SaveChanges();
           
        }

        public SubCategory UpdateSubCategoryWithCategoryId(SubCategory model)
        { 
            var SubCategory = _context.SubCategories.FirstOrDefault(s => s.SubCategoryId == model.SubCategoryId);
            if (SubCategory == null) return model;
            var category = _context.Categories.SingleOrDefault(p => p.CategoryId == model.CategoryId);
            if (category == null)
            {
                SubCategory.SubCategoryName = model.SubCategoryName;               
                _context.SaveChanges();
                return SubCategory;
                
            }
            else
            {
                SubCategory.SubCategoryName = model.SubCategoryName;
                SubCategory.CategoryId = model.CategoryId;
                _context.SaveChanges();
                return SubCategory;
            }

        }

       
    }
}