using Sahika.DataAccess.Abstract;
using Sahika.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Sahika.DataAccess.Concrete
{
    public class EfCategoryRepository :EfRepositoryBase<Category, SahikaContext>, ICategoryRepository
    {

        private SahikaContext _context;
        public EfCategoryRepository(SahikaContext context) : base(context)
        {
            _context = context;
        }

       

        public Category GetCategoryWithPosts(int categoryId)
        {//DAha bitmedi
            var category = _context.Categories.Include(c => c.Posts).FirstOrDefault(c => c.CategoryId == categoryId); 
            return category;
        }

        public Category GetCategoryWithSubs(int categoryId)
        {
            var category = _context.Categories.Include(c => c.SubCategories).FirstOrDefault(c => c.CategoryId == categoryId);
            return category;
        }

        public bool CategoryExists(int categoryId)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if (category == null)
                return true;
            return false;
        }

        public List<Category> GetAll()
        {
            
            return _context.Categories.ToList();
        }

        public List<int> GetCategoryIds()
        {
            return _context.Categories.Select(c => c.CategoryId).ToList();
        }
    }
}