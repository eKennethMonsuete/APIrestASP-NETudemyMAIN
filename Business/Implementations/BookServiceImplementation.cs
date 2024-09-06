using APIrestASP_NETudemy.Model;
using APIrestASP_NETudemy.Repository;
using RestASPNETErudio.Model;

namespace APIrestASP_NETudemy.Business.Implementations
{
    public class BookServiceImplementation : IBookBusiness
    {

        private readonly IBookRepository _repository;

        public BookServiceImplementation(IBookRepository repository)
        {
            _repository = repository;
        
        } 
        
        public List<Book> FindAll()
        {
            return _repository.FindAll();
        } 
        
        public Book FindByID(long id)
        {
            return _repository.FindByID(id);
        }


        public Book Create(Book book)
        {
            return _repository.Create(book);
        }
  

        public Book Update(Book book)
        {
            return _repository.Update(book);
        }
        
        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
