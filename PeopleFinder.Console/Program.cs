using System;
using System.Linq;
using PeopleFinder.Context;
using PeopleFinder.Model;

namespace PeopleFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new PersonContext())
            {
                // Add a new person
                Console.Write("Enter the new person's first name: ");
                string firstName = Console.ReadLine();

                Console.Write("Enter the new person's last name: ");
                string lastName = Console.ReadLine();

                Console.Write("Enter the new person's age: ");
                int age;
                int? dbAge = null;

                if (int.TryParse(Console.ReadLine(), out age))
                {
                    dbAge = age;
                }

                Console.Write("Enter the new person's address: ");
                string address = Console.ReadLine();

                Console.Write("Enter the new person's interests (comma delimited): ");
                string interests = Console.ReadLine();

                Person person = new Person
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Age = (int)dbAge,
                    Address = address,
                    Interests = interests
                };
                db.Persons.Add(person);
                db.SaveChanges();

                // Display all Blogs from the database
                var query = from p in db.Persons
                            orderby p.FirstName
                            select p;

                Console.WriteLine("All people in the database:");
                Console.WriteLine(Environment.CurrentDirectory + @"\people.db");
                foreach (var item in query)
                {
                    Console.WriteLine(item.FirstName + " " + item.LastName);
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
