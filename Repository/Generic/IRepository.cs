using APIrestASP_NETudemy.Model.Base;
using RestASPNETErudio.Model;

namespace RestASPNETErudio.Business.Implementations
{
    public interface IRepository<T> where T : BaseEntity
    {

        T Create(T item);

        T FindByID(long id);

        List<T> FindAll(); 

        T Update(T item);

        void Delete(long id);   

        bool Exists(long id);   
       
    }
}
