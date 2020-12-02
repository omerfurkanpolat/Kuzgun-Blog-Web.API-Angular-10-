using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sahika.Dtos
{
    public class UserForConfirmEmailDTO
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Code { get; set; }
    }
}