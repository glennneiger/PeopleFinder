using PeopleFinder.Model;
using System.Collections.Generic;

namespace PeopleFinder.Service
{
    public interface IPersonService
    {
        IEnumerable<Person> SelectAll();
        Person SelectByID(int id);
        IEnumerable<Person> Search(string input);
        int Add(Person person);
    }
}
