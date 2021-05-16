using System.Collections.Generic;
using ColossalCave.Objects;
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
            Inventory.Add(Items.Get<Bottle>());
            IList<string> results = parser.Parse("eat bottle");
            Assert.AreEqual("That's plainly inedible.", results[0]);
        }

        [Test]
        public void should_eat_inedible_things()
        {
            var bottle = Items.Get<Bottle>();
            bottle.IsEdible = true;

            Inventory.Add(bottle);
            IList<string> results = parser.Parse("eat bottle");
            Assert.AreEqual("You eat the small bottle. Not bad.", results[0]);
        }
    }
}
