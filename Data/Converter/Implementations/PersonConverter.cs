﻿using APIrestASP_NETudemy.Data.Converter.Contract;
using APIrestASP_NETudemy.Data.VO;
using RestASPNETErudio.Model;

namespace APIrestASP_NETudemy.Data.Converter.Implementations
{

    public class PersonConverter : IParser<PersonVO, Person>, IParser<Person, PersonVO>
    {
        public Person Parse(PersonVO origin)
        {
            if (origin == null) return null;
            return new Person
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Adress = origin.Adress,
                Gender = origin.Gender,
            };
        }

        

        public PersonVO Parse(Person origin)
        {
            if (origin == null) return null;
            return new PersonVO
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Adress = origin.Adress,
                Gender = origin.Gender,
            };
        }

        public List<PersonVO> Parse(List<Person> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
        
        public List<Person> Parse(List<PersonVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
