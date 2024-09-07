using System.ComponentModel.DataAnnotations.Schema;

namespace APIrestASP_NETudemy.Model.Base
{
    public class BaseEntity
    {
        [Column("id")]
        public long Id { get; set; }
            
         
        
    }
}
