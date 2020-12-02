using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sahika.Dtos
{
    public class SubCategoryForCreationDTO
    {
        [Required]
        public string SubCategoryName { get; set; }
        [Required]
        public int CategoryId { get; set; }

        

    }

    public class SubCategoryForReturnDTO
    {

        public int SubCategoryId { get; set; }
       
        public string SubCategoryName { get; set; }

    }
}