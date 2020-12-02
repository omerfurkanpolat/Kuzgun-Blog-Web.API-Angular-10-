using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using static Sahika.Models.IdentityModels;

namespace Sahika.Models
{
    public class PostComment:BaseEntity
    {
        public int PostCommentId { get; set; }     
       
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int? PostId { get; set; }
        [ForeignKey("PostId ")]
        public Post Post { get; set; }
        public string Comment { get; set; }
        public DateTime DateCreated { get; set; }
    }
}