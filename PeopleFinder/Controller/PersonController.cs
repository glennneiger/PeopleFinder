using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PeopleFinder.Model;
using PeopleFinder.Service;

namespace PeopleFinder.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        // GET /api/person
        [HttpGet]
        public IEnumerable<Person> SelectAll()
        {
            return _personService.SelectAll();
        }

        // GET /api/person/{id}
        [HttpGet("{id}")]
        public Person SelectByID(int id)
        {
            return _personService.SelectByID(id);
        }


        // GET /api/person/search/{input}
        [HttpGet("search/{input}")]
        public IEnumerable<Person> Search(string input)
        {
            return _personService.Search(input);
        }

        // POST /api/person
        [HttpPost]
        public IActionResult Add([FromBody] Person person)
        {
            if (person == null)
            {
                return BadRequest();
            }

            _personService.Add(person);
            return Ok(person);
        }
    }
}