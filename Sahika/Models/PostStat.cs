using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sahika.Models
{
    public class PostStat:BaseEntity
    {
        [ForeignKey("Post")]
        public int PostStatId { get; set; }
        public int Views { get; set; }
        public int Claps { get; set; }
        public DateTime? DateAdded { get; set; }
        public Post Post { get; set; }
    }
}