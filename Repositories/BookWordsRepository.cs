using System;
using System.Collections.Generic;

namespace Library.Models
{
    // Stores the words in a book. These should always be returned in "Capital" case. All matching should be case-insensitive (e.g. searching "cap" would find "Capital")
    public class BookWordsRepository
    {
        // Add words parsed from the given text into this repository
        public void Add(string text){
            throw new NotImplementedException();
        }

        // Return the number of appearances of a specified word in this book
        public int GetCount(string word)
        {
            throw new NotImplementedException();
        }

        // Return a list of words which start with the specified prefix in this book
        public List<WordCount> Search(string query)
        {
            throw new NotImplementedException();
        }

        // Return the top-10 most common words in this book, along with their counts, in descending order of appearance.
        public List<WordCount> MostCommonWords()
        {
            throw new NotImplementedException();
        }

    }
}