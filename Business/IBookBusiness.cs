using APIrestASP_NETudemy.Model;
using RestASPNETErudio.Model;

namespace APIrestASP_NETudemy.Business
{
    public interface IBookBusiness
    {
        Book Create(Book book);

        Book FindByID(long id);

        List<Book> FindAll();

        Book Update(Book book);

        void Delete(long id);


    }
}
