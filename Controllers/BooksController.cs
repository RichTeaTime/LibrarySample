using Library.Models;
using Library.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    /*
    * 
        BE – Simple command line app that reads in a text file, counts the number of words and returns the most common ten.

        UI – Very simple HTML app which shows how to retrieve some data and display it on the page. 
        i.e. a HTML page including a <script> tag which loads a JS ‘app’ file via ES6 type=”module”.
                        
    */
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private static readonly BookRepository repository = new BookRepository();

        [HttpGet]
        public IEnumerable<Book> Get()
        {
            // Return the list of book ids and titles
            throw new NotImplementedException();
        }

        [HttpGet("{id:int}")]
        public IActionResult GetTopWords(int id)
        {
            // Return a list of the 10 most common words (>3 letters) 
            throw new NotImplementedException();
        }

        [HttpGet("{id:int}/count/{word}")]
        public IActionResult WordCount(int id, string word)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id:int}/search/{query}")]
        public IActionResult SearchForWord(int id, string query)
        {
            throw new NotImplementedException();
        }
    }
}