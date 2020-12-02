using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sahika.Dtos
{
    public class UserForChangeEmailDTO
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        public string ImageUrl { get; set; }
    }

    public class UserForChangeProfilePictureDTO
    {
        [Required]
        public string ImageUrl { get; set; }
    }
}