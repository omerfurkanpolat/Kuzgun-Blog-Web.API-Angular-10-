using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Sahika.Models.IdentityModels;

namespace Sahika.Dtos
{
    public class RoleForReturnDTO
    {
        public List<ApplicationUser> Users { get; set; }
        public int UserCount { get; set; }
    }
}