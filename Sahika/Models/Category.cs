using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sahika.Models
{
    public class Category : BaseEntity
    {
        public Category()
        {
            SubCategories = new List<SubCategory>();
            Posts = new List<Post>();
            DateCreated = DateTime.Now;
        }
        [Required]
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        
       
        public DateTime DateCreated { get; set; }

        public List<SubCategory> SubCategories { get; set; }
        public ICollection<Post> Posts { get; set; }

    }
}