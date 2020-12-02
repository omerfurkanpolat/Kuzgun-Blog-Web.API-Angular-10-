using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sahika.Dtos
{
    public class UserForLoginDTO
    {
        public UserForLoginDTO()
        {
            LastActive = DateTime.Now;
        }
        

        [Required(ErrorMessage ="Kulllanıcı adı alanı boş bırakılamaz")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Şifre alamı boş bırakalamaz")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        
        public DateTime LastActive { get; set; }
    }
}