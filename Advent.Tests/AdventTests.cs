using System.Collections.Generic;
using ColossalCave.MyRooms;
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

        [Test]
        public void should_take_held_object_if_in_room()
        {
            // eat command requires that object be in players inventory
            // here it's not, but it is in the room - so take it for
            // the player automatically
            Location = Rooms.Get<InsideBuilding>();
            Assert.IsFalse(Inventory.Contains("food"));
            IList<string> results = parser.Parse("eat food");
            Assert.AreEqual("(first taking the tasty food)", results[0]);
            Assert.AreEqual("Delicious!", results[1]);
        }

        
    }
}
