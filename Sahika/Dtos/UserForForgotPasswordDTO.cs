using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sahika.Dtos
{
    public class UserForForgotPasswordDTO
    {
        [Required]
        [EmailAddress]
         public string Email { get; set; }
    }
}