using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sahika.Models
{
    public class SubCategory:BaseEntity
    {  //EĞER YENİ BİR PROPERTY TANIMLANIRSA REPOSİTORY'İDE Kİ UPDATE METODU GÖZDEN GEÇİRİLMELİ
        public SubCategory()
        {
           
            Posts = new List<Post>();
            DateCreated = DateTime.Now;
        }
        public int SubCategoryId { get; set; }

        [Required]
        public string SubCategoryName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        //belki buraya bi
        public DateTime? DateCreated { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
     
        public ICollection<Post> Posts { get; set; }
    }
}