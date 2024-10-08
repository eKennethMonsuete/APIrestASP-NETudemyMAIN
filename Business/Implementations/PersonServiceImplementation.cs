﻿using APIrestASP_NETudemy.Business;
using APIrestASP_NETudemy.Data.Converter.Implementations;
using APIrestASP_NETudemy.Data.VO;

using RestASPNETErudio.Model;

using System;

namespace RestASPNETErudio.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IRepository<Person> _repository;

        private readonly PersonConverter _converter;

        //APIrestASP_NETudemy.Business.

        public PersonBusinessImplementation(IRepository<Person> repository) {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public List<PersonVO> FindAll()
        {

            return _converter.Parse(_repository.FindAll());
        }



        public PersonVO FindByID(long id)
        {
            return _converter.Parse(_repository.FindByID(id));
        }

        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
        }

        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);
        }

       

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

       
       
    }
}
