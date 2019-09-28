using Microsoft.EntityFrameworkCore;
using PeopleFinder.Model;
using System;

namespace PeopleFinder.Context
{
    public class PersonContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) 
            => options.UseSqlite($"Data Source=" + Environment.CurrentDirectory + @"\people.db");
    }
}
