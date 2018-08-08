using System.Collections.Generic;
using Adventure.Net;
using NUnit.Framework;

namespace Advent.Tests
{
    [TestFixture]
    public class GrammarBuilderTests
    {

        [Test]
        public void object_only()
        {
            var tokens = new List<string>() { "x" };
            var grammarBuilder = new GrammarBuilder(tokens);
            
            var results = grammarBuilder.Build();
            Assert.AreEqual(4, results.Count);
            Assert.IsTrue(results.Contains("<noun>"));
            Assert.IsTrue(results.Contains("<held>"));
            Assert.IsTrue(results.Contains("<multi>"));
            Assert.IsTrue(results.Contains("<multiheld>"));
            
        }

        [Test]
        public void preposition_first()
        {
            var tokens = new List<string>() { "on", "x" };
            var grammarBuilder = new GrammarBuilder(tokens);
            var results = grammarBuilder.Build();
            Assert.AreEqual(4, results.Count);
            Assert.IsTrue(results.Contains("on <noun>"));
            Assert.IsTrue(results.Contains("on <held>"));
            Assert.IsTrue(results.Contains("on <multi>"));
            Assert.IsTrue(results.Contains("on <multiheld>"));
            
        }

        [Test]
        public void with_indirect_object()
        {
            var tokens = new List<string>() { "x", "with", "y" };
            var grammarBuilder = new GrammarBuilder(tokens);
            var results = grammarBuilder.Build();
            Assert.AreEqual(8, results.Count);
            Assert.IsTrue(results.Contains("<noun> with <noun>"));
            Assert.IsTrue(results.Contains("<held> with <noun>"));
            Assert.IsTrue(results.Contains("<multi> with <noun>"));
            Assert.IsTrue(results.Contains("<multiheld> with <noun>"));
            Assert.IsTrue(results.Contains("<noun> with <held>"));
            Assert.IsTrue(results.Contains("<held> with <held>"));
            Assert.IsTrue(results.Contains("<multi> with <held>"));
            Assert.IsTrue(results.Contains("<multiheld> with <held>"));

        }
    }
}
