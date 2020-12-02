using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sahika.Dtos
{
    public class PaginationForListPostDTO
    {
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
    }
}