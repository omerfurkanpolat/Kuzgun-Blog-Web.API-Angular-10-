using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sahika.Dtos
{
    public class UserForCreationDTO
    {
        public UserForCreationDTO()
        {
            LastActive = DateTime.Now;
        }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string UserRole { get; set; }
        public DateTime LastActive { get; set; }
    }
}