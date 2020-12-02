using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using static Sahika.Models.IdentityModels;

namespace Sahika.Models
{
    public class Message:BaseEntity
    {
        [Required]
        [Key]
        public int MessageId { get; set; }
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