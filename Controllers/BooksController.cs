using Library.Repository;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Library.Controllers
{
    [System.Web.Mvc.RoutePrefix("api/books")]    
    public class BooksController : ApiController
    {
        BooksRepository bookRepository = new BooksRepository();
        /// <summary>
        /// Book Controller constructor
        /// </summary>
        public BooksController()
        { }
        /// <summary>
        /// Method to get books
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {            
            var books = bookRepository.GetBookNames(); 
            if(books == null)
            {
                return NotFound();
            }
            return Json(books.ToList());
        }
        /// <summary>
        /// Get Word counts per book 
        /// </summary>
        /// <param name="Id"></param> Id is primary key of the Book class
        /// <returns></returns>
        public IHttpActionResult Get(int Id)
        {

            var wordCounts = bookRepository.GetWordCounts(Id);
            if (wordCounts == null)
            {
                return NotFound();
            }
            return Json(wordCounts);
        }
        /// <summary>
        /// Get search words
        /// </summary>
        /// <param name="Id"></param> Book ID
        /// <param name="query"></param> Word to search in the book
        /// <returns></returns>
        public IHttpActionResult Get(int Id, [FromUri] string query)
        {
            if (query.Length >= 3)
            {
                var wordCounts = bookRepository.SearchWordCounts(Id, query);
                if (wordCounts == null || wordCounts.Count() == 0)
                {
                    return NotFound();
                }
                return Json(wordCounts);
            }
            else
                return StatusCode(HttpStatusCode.LengthRequired);                
            
        }
    }
}
