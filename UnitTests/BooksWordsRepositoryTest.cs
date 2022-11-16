using Library.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Library.UnitTests
{
    [TestClass]
    public class BooksWordsRepositoryTest
    {
        private const string SAMPLE_TEXT = @"
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum non mattis magna. Proin rutrum, erat in tincidunt finibus, urna dolor maximus eros, ac dapibus lorem lorem nec urna.
Mauris vulputate egestas iaculis. Nullam accumsan ante laoreet libero elementum, vitae venenatis lacus fermentum. Orci varius natoque penatibus et magnis dis parturient montes, nascetur
ridiculus mus. Maecenas sodales ut nisi et faucibus. Donec maximus lacinia tellus, eget pharetra elit condimentum at.

Nunc ut sem imperdiet, posuere sapien ac, dictum orci. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nam feugiat ante ut metus efficitur,
eu rutrum purus sodales. In maximus eleifend dapibus. Aliquam luctus posuere fringilla. Ut tincidunt gravida maximus. Aenean vitae luctus diam. In hac habitasse platea dictumst. Vestibulum
commodo luctus est in dictum. Aenean id accumsan elit.

Morbi posuere urna ut augue accumsan mattis. Aliquam erat volutpat. In mattis est libero, nec blandit turpis ullamcorper nec. Nullam gravida leo ligula, id interdum justo ultricies ac.
Curabitur ultrices leo ultrices magna tempus lacinia. Donec gravida eu-erat venenatis blandit. Cras id sapien mauris. Proin in massa rhoncus, vulputate urna eget, consequat enim. In at
volutpat mi, non viverra lorem. Praesent pharetra luctus cursus. Vestibulum pellentesque massa id lacus lobortis vehicula. Donec venenatis malesuada leo, vitae sollicitudin nibh imperdiet
eu. Aenean euismod ante at ipsum interdum, sit amet rhoncus tortor hendrerit. Ut elementum tortor a diam vehicula, at varius quam dictum. Suspendisse id nulla purus. Etiam et tempor tortor,
vel elementum risus.";

        private BookWordsRepository repository;

        public BooksWordsRepositoryTest() {
            repository = new BookWordsRepository();
            repository.Add(SAMPLE_TEXT);
        }

        [TestMethod]
        public void CountWords()
        {
            Assert.AreEqual(4, repository.GetCount("Lorem"));
            Assert.AreEqual(2, repository.GetCount("DIAM"));
            Assert.AreEqual(3, repository.GetCount("erat"));   // ignore -

            Assert.AreEqual(0, repository.GetCount("ut"));     // minimum length = 3 so shouldn't find any
        }

        [TestMethod]
        [DataRow("Lorem", 1, "Lorem", 4)]
        [DataRow("sa", 1, "Sapien", 2)]
        [DataRow("blanditTESTmore", 0)]
        [DataRow("in", 2, "Inceptos", 1, "Interdum", 2)]   // short words are discarded
        public void SearchWords(string query, int expectCount, params object[] expected)
        {
            var result = repository.Search(query);
            Assert.AreEqual(expectCount, result.Count);

            for (int i = 0; i < expected.Length; i += 2)
            {
                var word = expected[i] as string;
                var expCount = Convert.ToInt32(expected[i + 1]);

                var actualCount = result.Single(r => r.Word == word).Count;
                Assert.AreEqual(expCount, actualCount);
            }
        }
    }
}
