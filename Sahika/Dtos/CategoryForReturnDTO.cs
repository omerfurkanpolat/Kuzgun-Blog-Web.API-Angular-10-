using Sahika.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sahika.Dtos
{
    public class CategoryForReturnDTO
    {
        public int CategoryId { get; set; }       
        public string CategoryName { get; set; }      
       
        public DateTime DateCreated { get; set; }

        public List<SubCategory> SubCategories { get; set; }

    }
}