using System.ComponentModel.DataAnnotations.Schema;

namespace APIrestASP_NETudemy.Data.VO
{
    public class BookVO
    {

        public long Id
        {
            get; set;
        }

        public string Author
        {
            get; set;
        }


        public decimal Price
        {
            get; set;
        }


        public DateTime LaunchDate
        {
            get; set;
        }


        public string Title
        {
            get; set;
        }
    }
}
