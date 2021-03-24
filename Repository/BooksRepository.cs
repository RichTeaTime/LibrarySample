using Library.Data;
using Library.Models;
using System.Collections.Generic;

namespace Library.Repository
{
    public class BooksRepository
    {
        private readonly string _location = @"Resources";
        FileContext fileContext;
        /// <summary>
        /// book repository constructor
        /// </summary>
        public BooksRepository()
        {
            fileContext = new FileContext(_location);
        }
        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="filePath"></param> File path of the text files
        public BooksRepository(string filePath)
        {
            fileContext = new FileContext(filePath);
        }
        /// <summary>
        /// Method to get book names
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Books> GetBookNames()
        {
            return fileContext.GetBookNames();
        }
        /// <summary>
        /// get word counts per book
        /// </summary>
        /// <param name="Id"></param> ID of the Book
        /// <returns></returns>
        public IEnumerable<WordCount> GetWordCounts(int Id)
        {
            return fileContext.GetWordCounts(Id);
        }
        /// <summary>
        /// get words per book by search word
        /// </summary>
        /// <param name="Id"></param> Book Id
        /// <param name="searchWord"></param> Word to search in the book
        /// <returns></returns>
        public IEnumerable<WordCount> SearchWordCounts(int Id, string searchWord)
        {
            return fileContext.SearchWordCounts(Id, searchWord);
        }
    }
}
