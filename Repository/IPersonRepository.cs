using RestASPNETErudio.Model;

namespace RestASPNETErudio.Business.Implementations
{
    public interface IPersonRepository
    {

        Person Create(Person person);

        Person FindByID(long id);

        List<Person> FindAll(); 

        Person Update(Person person);

        void Delete(long id);   

        bool Exists(long id);   
       
    }
}
