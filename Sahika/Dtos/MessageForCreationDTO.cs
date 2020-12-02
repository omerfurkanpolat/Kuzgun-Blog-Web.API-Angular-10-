using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sahika.Dtos
{
    public class MessageForCreationDTO
    {
        [Required]
        public string MessageHeader { get; set; }
        [Required]
        public string MessageBody { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string FullName { get; set; }

        public string Answer { get; set; }
    }

   
}