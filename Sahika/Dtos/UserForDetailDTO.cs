using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using static Sahika.Models.IdentityModels;

namespace Sahika.Dtos
{
    public class UserForDetailDTO
    {
       
        public int Id { get; set; }
        public string  UserName { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateRegistered { get; set; }
        public DateTime LastActive { get; set; }
        public string UserRole { get; set; }
    }
}