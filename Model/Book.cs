using APIrestASP_NETudemy.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIrestASP_NETudemy.Model
{
    [Table("books")]
    public class Book : BaseEntity
    {
        
        
        [Column("author")]
        public string Author { get; set; }
        
        [Column("price")]
        public decimal Price { get; set; }

        [Column("launch_date")]
        public DateTime LaunchDate { get; set; }
        
        [Column("title")]
        public string Title { get; set; }
        
        
   


    }
}
