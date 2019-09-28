using PeopleFinder.Context;
using PeopleFinder.Model;
using System.Collections.Generic;
using System.Linq;

namespace PeopleFinder.Service
{
    public class PersonService : IPersonService
    {
        readonly PersonContext _pc;

        public PersonService(PersonContext pc)
        {
            _pc = pc;
        }

        public IEnumerable<Person> SelectAll()
        {
            var query = from p in _pc.Persons
                    orderby p.FirstName
                    select p;

            return query.ToList();
        }

        public Person SelectByID(int id)
        {
            return (from p in _pc.Persons
                    where p.PersonID == id
                    orderby p.FirstName
                    select p).FirstOrDefault();
        }

        public IEnumerable<Person> Search(string input)
        {
            return from p in _pc.Persons
                   where p.FirstName == input || p.LastName == input
                   orderby p.FirstName
                   select p;
        }

        public int Add(Person person)
        {
            _pc.Persons.Add(person);
            _pc.SaveChanges();
            return person.PersonID;
        }
    }
}
