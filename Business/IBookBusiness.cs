using APIrestASP_NETudemy.Data.VO;
using APIrestASP_NETudemy.Model;
using RestASPNETErudio.Model;

namespace APIrestASP_NETudemy.Business
{
    public interface IBookBusiness
    {
        BookVO Create(BookVO book);

        BookVO FindByID(long id);

        List<BookVO> FindAll();

        BookVO Update(BookVO book);

        void Delete(long id);


    }
}
