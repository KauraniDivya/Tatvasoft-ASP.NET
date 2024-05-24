using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        static List<Books> books = new List<Books>();
        // GET: api/<BooksController>
        [HttpGet]
        public IEnumerable<Books> Get()
        {
            return books;
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public Books Get(int id)
        {
            return books.FirstOrDefault(s => s.Id == id);
        }

        // POST api/<BooksController>
        [HttpPost]
        public void Post([FromBody] Books value)
        {
            books.Add(value);
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Books value)
        {
           int i = books.FindIndex(s => s.Id == id);
           if (i>=0)
                books[i] = value;
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            books.RemoveAll(s => s.Id == id);
        }
    }
}
