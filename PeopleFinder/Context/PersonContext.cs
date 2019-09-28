using Microsoft.EntityFrameworkCore;
using PeopleFinder.Model;
using System;

namespace PeopleFinder.Context
{
    public class PersonContext : DbContext
    {
        public PersonContext()
        {
        }

        public PersonContext(DbContextOptions<PersonContext> options) : base(options)
        {
        }

        public virtual DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) 
            => options.UseSqlite($"Data Source=" + Environment.CurrentDirectory + @"\people.db");
    }
}
