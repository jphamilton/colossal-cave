using System.Collections.Generic;
using ColossalCave.Places;
using Adventure.Net;
using NUnit.Framework;

namespace Advent.Tests
{
    [TestFixture]
    public class ParserTests : AdventTestFixture
    {
        [Test]
        public void thats_not_a_verb_I_recognize()
        {
            IList<string> results = parser.Parse("snarky snark");
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("That's not a verb I recognize.", results[0]);
        }

        [Test]
        public void i_beg_your_pardon()
        {
            IList<string> results = parser.Parse("");
            Assert.AreEqual("I beg your pardon?", results[0]);

            results = parser.Parse(null);
            Assert.AreEqual("I beg your pardon?", results[0]);
        }

        

        
    }
}
