using System.Collections.Generic;
using ColossalCave.MyObjects;
using Adventure.Net;
using NUnit.Framework;

namespace Advent.Tests.Verbs
{
    [TestFixture]
    public class EatTests : AdventTestFixture
    {
        [Test]
        public void cannot_eat_inedible_things()
        {
            Inventory.Add(Objects.Get<Bottle>());
            IList<string> results = parser.Parse("eat bottle");
            Assert.AreEqual("That's plainly inedible.", results[0]);
        }

        [Test]
        public void should_eat_inedible_things()
        {
            var bottle = Objects.Get<Bottle>();
            bottle.IsEdible = true;

            Inventory.Add(bottle);
            IList<string> results = parser.Parse("eat bottle");
            Assert.AreEqual("You eat the small bottle. Not bad.", results[0]);
        }
    }
}
