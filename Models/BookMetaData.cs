using System.IO;

namespace Library.Models
{
    
    public class Books
    {
        private string _bookName = string.Empty;
        public int Id { get; set; }
        public string BookName
        {
            get
            {
                return _bookName;
            }
            set
            {
                Location = value;
                _bookName = Path.GetFileNameWithoutExtension(value);
            }
        }
        internal string Location
        {
            get;
            private set;
        }
    }

    public class WordCount
    {
        public string Word { get; set; }
        public int Count { get; set; }
        public WordCount(string wd, int ct)
        {
            Word = wd;
            Count = ct;
        }
    }
}
