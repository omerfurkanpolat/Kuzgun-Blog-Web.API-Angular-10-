using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using static Sahika.Models.IdentityModels;

namespace Sahika.Models
{
    public class Post : BaseEntity
    {
        public Post()
        {
            
            FavouritePosts = new List<FavouritePost>();
            PostComments = new List<PostComment>();
            DateCreated = DateTime.Now;
                
        }

        [Key]
        public int PostId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public DateTime DateCreated { get; set; }
        public string ImageUrl { get; set; }
        public bool IsPublished { get; set; } = true;
       
        public PostStat PostStat { get; set; }
        [Required]
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        public List<FavouritePost> FavouritePosts { get; set; }
        public List<PostComment> PostComments { get; set; }
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public int? SubCategoryId { get; set; }
        [ForeignKey("SubCategoryId")]
        public virtual SubCategory SubCategory { get; set; }
    }
}