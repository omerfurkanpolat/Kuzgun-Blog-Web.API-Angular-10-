using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sahika.Dtos
{
    public class PostForUpdateDTO
    {
        [Required]
        public int PostId { get; set; }
  
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public int SubCategoryId { get; set; }
        [Required]
        public int UserId { get; set; }
        public bool IsPublished { get; set; }
        public int CategoryId { get; set; }

    }
}