using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PeopleFinder.Controller;
using PeopleFinder.Model;
using PeopleFinder.Service;
using System.Collections.Generic;

namespace PeopleFinder.Test
{
    [TestClass]
    public class PersonControllerTest
    {
        private readonly Person expectedPerson;
        private readonly List<Person> expectedPersonList;

        public PersonControllerTest()
        {
            expectedPerson = new Person
            {
                PersonID = 1,
                FirstName = "Ryan",
                LastName = "Gillette",
                Age = 38,
                Address = "Webster NY"
            };

            expectedPersonList = new List<Person>(){
                new Person {
                    PersonID = 1,
                    FirstName = "Ryan",
                    LastName = "Gillette",
                    Age = 38,
                    Address = "Webster NY"
                },
                new Person {
                    PersonID = 2,
                    FirstName = "Joe",
                    LastName = "Smith",
                    Age = 26,
                    Address = "Tampa FL"
                }
            };
        }

        [TestMethod]
        public void SelectAll_ReturnsList()
        {
            // Arrange
            var mockPersonService = new Mock<IPersonService>();
            mockPersonService
                .Setup(x => x.SelectAll())
                .Returns(expectedPersonList);
            var personController = new PersonController(mockPersonService.Object);

            // Act
            var response = personController.SelectAll();

            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(expectedPersonList, response);
        }

        [TestMethod]
        public void SelectByID_ReturnsPerson()
        {
            // Arrange
            var mockPersonService = new Mock<IPersonService>();
            mockPersonService
                .Setup(x => x.SelectByID(1))
                .Returns(expectedPerson);
            var personController = new PersonController(mockPersonService.Object);

            // Act
            var response = personController.SelectByID(1);

            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(expectedPerson, response);
        }

        [TestMethod]
        public void Search_ReturnsFilteredList()
        {
            // Arrange
            var mockPersonService = new Mock<IPersonService>();
            mockPersonService
                .Setup(x => x.Search("Ryan"))
                .Returns(expectedPersonList);
            var personController = new PersonController(mockPersonService.Object);

            // Act
            var response = personController.Search("Ryan");

            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(expectedPersonList, response);
        }

        [TestMethod]
        public void Add_ReturnsCreatedPerson()
        {
            // Arrange
            var mockPersonService = new Mock<IPersonService>();
            mockPersonService
                .Setup(x => x.Add(expectedPerson))
                .Returns(expectedPerson.PersonID);
            var personController = new PersonController(mockPersonService.Object);

            // Act
            var actionResult = personController.Add(expectedPerson) as OkObjectResult;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(200, actionResult.StatusCode);
            Assert.AreEqual(expectedPerson, actionResult.Value);
        }

        [TestMethod]
        public void Add_BadRequest()
        {
            // Arrange
            var mockPersonService = new Mock<IPersonService>();
            var personController = new PersonController(mockPersonService.Object);

            // Act
            var actionResult = personController.Add(null);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }
    }
}
