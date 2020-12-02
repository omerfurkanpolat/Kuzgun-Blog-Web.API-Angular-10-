using Sahika.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sahika.Dtos
{
    public class CategoryForCreationDTO
    {
        public int Id { get; set; }
        [Required] 
        public string CategoryName { get; set; }
       
        public DateTime DateCreated { get; set; }

    }
}