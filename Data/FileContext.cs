using Library.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library.Data
{
    public class FileContext
    {
        List<Books> files = new List<Books>();
        private readonly string _path = string.Empty;
        public FileContext(string path)
        {
            _path = path;
        }

        /// <summary>
        /// Method which get all the books from given path
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Books> GetBookNames()
        {
            //string filePath = System.Web.HttpContext.Current.Request.MapPath(_path);
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _path);
            int cnt = 1;
            List<Books> files = new List<Books>();
            foreach (var f in Directory.EnumerateFiles(filePath).OrderBy(o => o))
            {
                files.Add(new Books { Id = cnt, BookName = f });
                cnt++;
            }
            return files;
        }

        /// <summary>
        /// Get the words and count per book Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IEnumerable<WordCount> GetWordCounts(int Id)
        {

            List<WordCount> wordsCount = new List<WordCount>();
            var searchList = new List<string>();
            try
            {
                string filePath = GetBookNames().FirstOrDefault(f => f.Id == Id).Location;
                string text = string.Join(" ", File.ReadLines(filePath).ToArray());
                Regex reg_exp = new Regex("[^a-zA-Z0-9]");
                text = reg_exp.Replace(text, " ");
                string[] source = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var result = new ConcurrentDictionary<string, int>();
                Parallel.ForEach(File.ReadLines(filePath, Encoding.UTF8), line =>
                {
                    line = reg_exp.Replace(line, " ");
                    var words = line.ToLowerInvariant().Split(new[] { ' ', }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var word in words.Where(s => s.Length >= 5))
                    {
                        result.AddOrUpdate(word, 1, (_, x) => x + 1);
                    }
                });
                foreach (var kv in result.OrderByDescending(x => x.Value).Take(10))
                    wordsCount.Add(new WordCount(kv.Key.ToPascalCase(), kv.Value));
            }
            catch (Exception ex)
            {
                //Log Error
            }
            return wordsCount;
        }
        /// <summary>
        /// Get the relevant word and count for the search word
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<WordCount> SearchWordCounts(int Id, string query)
        {
            string fileName = GetBookNames().FirstOrDefault(f => f.Id == Id).Location;
            List<WordCount> words = new List<WordCount>();
            var searchList = new List<string>();
            try
            {
                List<string> lst = File.ReadLines(fileName).ToList();
                string text = string.Join(" ", lst.ToArray());
                Regex reg_exp = new Regex("[^a-zA-Z0-9]");
                text = reg_exp.Replace(text, " ");
                string[] source = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                searchList.Add(query.ToLowerInvariant());
                var matchQuery = source.Where(r => searchList.Any(f => r.StartsWith(f)));
                var cpt = matchQuery.Distinct().Select(x => x).ToList();
                foreach (var x in cpt)
                {
                    var match = from wd in source
                                where wd.ToLowerInvariant() == x.ToString().ToLowerInvariant()
                                select wd;
                    words.Add(new WordCount(x.ToString().ToPascalCase(), match.Count()));
                }
            }
            catch (Exception ex)
            {
                //Log Error
            }
            return words;
        }
    }

    internal static class StringExtension
    {
        public static string ToPascalCase(this string str)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > 1)
            {
                return char.ToUpperInvariant(str[0]) + str.Substring(1).ToLowerInvariant();
            }
            return str;
        }
    }
}
