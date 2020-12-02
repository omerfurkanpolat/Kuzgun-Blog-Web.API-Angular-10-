using Sahika.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sahika.Dtos
{
    public class PostForDetailDTO
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime DateCreated { get; set; }
        public string ImageUrl { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int Views { get; set; }
        public int Claps { get; set; }
        public IEnumerable<List<string>> Comment { get; set; }
        public IEnumerable<List<string>>   CommentUser { get; set; }

    }
}