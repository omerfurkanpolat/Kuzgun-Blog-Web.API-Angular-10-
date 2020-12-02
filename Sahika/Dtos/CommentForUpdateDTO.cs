using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sahika.Dtos
{
    public class CommentForUpdateDTO
    {
     [Required]
     public string comment { get; set; }
    }
}