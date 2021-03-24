using Library.Repository;
using NUnit.Framework;
using System.Linq;

namespace Library.Tests
{
    [TestFixture]
    public class LibraryTests
    {
        private const string SAMPLE_TEXT = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. 
                Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate 
                velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

        private const string testFileLocation = @"UnitTests\\Resources";
        private BooksRepository repository;
        /// <summary>
        /// Constructor
        /// </summary>
        public LibraryTests()
        {
            repository = new BooksRepository(testFileLocation);
        }
        /// <summary>
        /// Test book count and name of the book 
        /// </summary>
        [Test]
        public void BookCountandNameTest()
        {
            var books = repository.GetBookNames();
            Assert.IsNotNull(books);
            Assert.AreEqual(1, books.Count());
            Assert.AreEqual("SampleTest", books.FirstOrDefault().BookName);
        }
        /// <summary>
        /// Invalid Book Id Test
        /// </summary>
        [Test]
        public void InvalidBookIdTest()
        {
            var words = repository.GetWordCounts(2);
            Assert.IsNotNull(words);
            Assert.AreEqual(0, words.Count());
        }
        /// <summary>
        /// Test to get 10 most common words
        /// </summary>
        [Test]
        public void MostCommonWordsTest()
        {
            var words = repository.GetWordCounts(1);
            Assert.IsNotNull(words);
            Assert.AreEqual(10, words.Count());
        }
        /// <summary>
        /// Test to search specific word and its count
        /// </summary>
        [Test]
        public void SearchKeywordTest()
        {
            var words = repository.SearchWordCounts(1, "dol");
            Assert.IsNotNull(words);
            Assert.IsTrue(words.All(s => s.Word.ToLowerInvariant().Contains("dol")));
            Assert.AreEqual(2, words.Count());
            Assert.GreaterOrEqual(2, words.FirstOrDefault().Count);
        }
        /// <summary>
        /// Test to check the response of Invalid keyword
        /// </summary>
        [Test]
        public void InvalidKeywordSearchTest()
        {
            var words = repository.SearchWordCounts(1, "abcd");
            Assert.IsNotNull(words);
            Assert.AreEqual(0, words.Count());
        }
        /// <summary>
        /// Test for PascalCaseKeyword Search
        /// </summary>
        [Test]
        public void PascalCaseKeywordSearchTest()
        {
            var words = repository.SearchWordCounts(1, "consectetur");
            Assert.IsNotNull(words);
            Assert.AreEqual(1, words.Count());
            Assert.IsTrue(words.All(s => s.Word.Contains("Consectetur")));
            Assert.IsFalse(words.All(s => s.Word.Contains("consectetur")));
        }
    }
}