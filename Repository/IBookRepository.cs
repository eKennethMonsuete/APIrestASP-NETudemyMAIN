using APIrestASP_NETudemy.Model;
using RestASPNETErudio.Model;

namespace APIrestASP_NETudemy.Repository
{
    public interface IBookRepository
    {

        Book Create(Book book);

        Book FindByID(long id);

        List<Book> FindAll();

        Book Update(Book book);

        void Delete(long id);

        bool Exists(long id);
    }
}
