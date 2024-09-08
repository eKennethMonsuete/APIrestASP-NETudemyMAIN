using APIrestASP_NETudemy.Data.Converter.Implementations;
using APIrestASP_NETudemy.Data.VO;
using APIrestASP_NETudemy.Model;
using APIrestASP_NETudemy.Repository;
using RestASPNETErudio.Business.Implementations;
using RestASPNETErudio.Model;
using System;

namespace APIrestASP_NETudemy.Business.Implementations
{
    public class BookServiceImplementation : IBookBusiness
    {

        private readonly IRepository<Book> _repository;

        private readonly BookConverter _converter;

        public BookServiceImplementation(IRepository<Book> repository)
        {
            _repository = repository;
            _converter = new BookConverter();

        } 
        
        public List<BookVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        } 
        
        public BookVO FindByID(long id)
        {
            return _converter.Parse(_repository.FindByID(id));
        }


        public BookVO Create(BookVO book)
        {
            var bookEntiry = _converter.Parse(book);
            bookEntiry = _repository.Create(bookEntiry);
            return _converter.Parse(bookEntiry);
        }
  

        public BookVO Update(BookVO book)
        {
            var bookEntiry = _converter.Parse(book);
            bookEntiry = _repository.Update(bookEntiry);
            return _converter.Parse(bookEntiry);
        }
        
        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
