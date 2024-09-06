using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIrestASP_NETudemy.Model
{
    [Table("books")]
    public class Book
    {
        [Column("id")]
        public long Id { get; set; }
        
        [Column("author"), Required]
        public string Author { get; set; }
        
        [Column("price"), Required]
        public decimal Price { get; set; }

        [Column("launch_date")]
        public DateTime LaunchDate { get; set; }
        
        [Column("title")]
        public string Title { get; set; }
        
        
   


    }
}
