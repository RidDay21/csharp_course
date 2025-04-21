using App.Practise2;
using NUnit.Framework;
using System.Collections.Generic;

namespace AppTests.Practise2
{
    public class NGramTests
    {
        [Test]
        public void BasicTest()
        {
            string text = "a b c d. b c d. e b c a d.";
            var expected = new Dictionary<string, string>
            {
                { "a", "b" },
                { "b", "c" },
                { "c", "d" },
                { "e", "b" },
                { "a b", "c" },
                { "b c", "d" },
                { "e b", "c" },
                { "c a", "d" }
            };

            var result = NGram.CreateNGramStatistic(text);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void BasicTestWithDifferentPunctMarks()
        {
            string text = "a b c d! b c d? e b c a d.";
            var expected = new Dictionary<string, string>
            {
                { "a", "b" },
                { "b", "c" },
                { "c", "d" },
                { "e", "b" },
                { "a b", "c" },
                { "b c", "d" },
                { "e b", "c" },
                { "c a", "d" }
            };

            var result = NGram.CreateNGramStatistic(text);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestMultipleContinuationsWithDifferentFrequencies()
        {
            string text =
                "Marcus likes apples. Marcus likes oranges. Marcus hates bananas. Marcus hates melons. Marcus hates melons";

            var expected = new Dictionary<string, string>
            {
                { "Marcus", "hates" },
                { "likes", "apples" },
                { "hates", "melons" },
                { "Marcus likes", "apples" },
                { "Marcus hates", "melons" }
            };

            var result = NGram.CreateNGramStatistic(text);

            Assert.AreEqual(expected, result);
        }
    }
}