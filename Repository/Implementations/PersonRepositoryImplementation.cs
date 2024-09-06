using APIrestASP_NETudemy.Business;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RestASPNETErudio.Business.Implementations;
using RestASPNETErudio.Model;
using RestASPNETErudio.Model.Context;
using System;

namespace RestASPNETErudio.Repository.Implementations
{

    public class PersonRepositoryImplementation : IPersonRepository
    {
        private MySQLContext _context;

        // o Repository ficou apenas para a persistencia 
        public PersonRepositoryImplementation(MySQLContext context) {
            _context = context;
        }

        public List<Person> FindAll()
        {

            return _context.Persons.ToList();
        }



        public Person FindByID(long id)
        {
            return _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
        }

        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
            return person;
        }

        public Person Update(Person person)
        {
            if (!Exists(person.Id)) return null;
            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(person);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return person;
        }

       

        public void Delete(long id)
        {
            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
            if (result != null)
            {
                try
                {
                    _context.Persons.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }

        

        public bool Exists(long id)
        {
            return _context.Persons.Any(p => p.Id.Equals(id));
        }
    }
}
