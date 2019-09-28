using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PeopleFinder.Context;
using PeopleFinder.Model;
using PeopleFinder.Service;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleFinder.Test
{
    [TestClass]
    public class PersonServiceTest {
        private readonly Person expectedPerson;

        public PersonServiceTest()
        {
            expectedPerson = new Person
            {
                FirstName = "Ryan",
                LastName = "Gillette",
                Age = 38,
                Address = "Webster NY"
            };
        }

        [TestMethod]
        public void Add_PersonAdded()
        {
            // Arrange
            var mockContext = new Mock<PersonContext>();
            var mockSet = new Mock<DbSet<Person>>();
            mockContext.Setup(x => x.Persons).Returns(mockSet.Object);
            var personService = new PersonService(mockContext.Object);

            // Act
            personService.Add(expectedPerson);

            //Assert
            mockSet.Verify(m => m.Add(It.IsAny<Person>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public async Task SelectAll_Persons()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<PersonContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            // Act
            using (var context = new PersonContext(options))
            {
                await context.Database.EnsureCreatedAsync();
                context.Persons.Add(expectedPerson);
                await context.SaveChangesAsync();
            }

            // Assert
            using (var context = new PersonContext(options))
            {
                var personService = new PersonService(context);
                var result = personService.SelectAll();
                Assert.AreEqual(expectedPerson.FirstName, result.LastOrDefault().FirstName);
            }
        }

        [TestMethod]
        public async Task SelectByID_Person()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<PersonContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            // Act
            using (var context = new PersonContext(options))
            {
                await context.Database.EnsureCreatedAsync();
                context.Persons.Add(expectedPerson);
                await context.SaveChangesAsync();
            }

            // Assert
            using (var context = new PersonContext(options))
            {
                var personService = new PersonService(context);
                var result = personService.SelectByID(expectedPerson.PersonID);
                Assert.AreEqual(expectedPerson.PersonID, result.PersonID);
            }
        }

        [TestMethod]
        public async Task Search_PersonRyan()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<PersonContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            // Act
            using (var context = new PersonContext(options))
            {
                await context.Database.EnsureCreatedAsync();
                context.Persons.Add(expectedPerson);
                await context.SaveChangesAsync();
            }

            // Assert
            using (var context = new PersonContext(options))
            {
                var personService = new PersonService(context);
                var result = personService.Search("Ryan");
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Count() > 0);
            }
        }
    }
}
