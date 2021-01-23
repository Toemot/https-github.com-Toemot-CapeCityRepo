using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    public class PeopleController : ApiController
    {
        List<Person> people = new List<Person>();

        public PeopleController()
        {
            people.Add(new Person { Id = 1, FirstName = "Ola", LastName = "Otoks" });
            people.Add(new Person { Id = 2, FirstName = "Bimpe", LastName = "Otoks" });
            people.Add(new Person { Id = 3, FirstName = "Ayomide", LastName = "Otoks" });
        }

        // GET: api/People
        public List<Person> Get()
        {
            return people;
        }

        // GET: api/People/5
        public Person Get(int id)
        {
            return people
                .Where(p => p.Id == id)
                .FirstOrDefault();
        }

        // POST: api/People
        public void Post(Person human)
        {
            people.Add(human);
        }

        // PUT: api/People/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/People/5
        public void Delete(int id)
        {
        }
    }
}
