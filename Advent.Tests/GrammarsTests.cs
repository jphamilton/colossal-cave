using System.Linq;
using Adventure.Net;
using NUnit.Framework;

namespace Advent.Tests
{
    public class GrammarsTests
    {
        [Test]
        public void preposition_gets_set()
        {
            var grammars = new Grammars
                {
                    {"<multi> [in,inside,into] <noun>", () => true}
                };

            var grammar = grammars.Single(x => x.Preposition == "in");
            grammar = grammars.Single(x => x.Preposition == "inside");
            grammar = grammars.Single(x => x.Preposition == "into");
        } 
    }
}