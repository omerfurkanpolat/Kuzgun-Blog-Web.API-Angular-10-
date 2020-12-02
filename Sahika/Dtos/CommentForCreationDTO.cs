using Sahika.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static Sahika.Models.IdentityModels;

namespace Sahika.Dtos
{
    public class CommentForCreationDTO
    {
        [Required]
        public string Comment { get; set; }

        [Required]

        public int UserId{ get; set; }
    }

    public class CommentForReturnDTO
    {
        public int PostCommentId { get; set; }

        public int UserId { get; set; }
        
        public int? PostId { get; set; }
      
        public string Comment { get; set; }
        public string UserName { get; set; }
        public bool Exists { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateCreated { get; set; }

    }
}