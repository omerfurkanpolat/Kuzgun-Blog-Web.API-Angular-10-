using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Sahika.Dtos
{
    public class PostForCreationDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        
        [Required]
        public string ImageUrl { get; set; }  
        [Required]
        public int SubCategoryId { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}