using APIrestASP_NETudemy.Business;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RestASPNETErudio.Model;
using RestASPNETErudio.Model.Context;
using System;

namespace RestASPNETErudio.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IPersonRepository _repository;

        //APIrestASP_NETudemy.Business.

        public PersonBusinessImplementation(IPersonRepository repository) {
            _repository = repository;
        }

        public List<Person> FindAll()
        {

            return _repository.FindAll();
        }



        public Person FindByID(long id)
        {
            return _repository.FindByID(id);
        }

        public Person Create(Person person)
        {
            
            return _repository.Create(person);
        }

        public Person Update(Person person)
        {
            return _repository.Update(person);
        }

       

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

       
       
    }
}
