using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using static Sahika.Models.IdentityModels;

namespace Sahika.Models
{
    public class FavouritePost : BaseEntity
    {

        public int FavouritePostId { get; set; }    
        public int?  PostId { get; set; }
        public int UserId { get; set; }  
        [ForeignKey("PostId ")]
        public Post Post { get; set; }
        public ApplicationUser User { get; set; }
    }
}