using System.Collections.Generic;
using ColossalCave.MyObjects;
using ColossalCave.MyRooms;
using Adventure.Net;
using NUnit.Framework;

namespace Advent.Tests.Verbs
{
    [TestFixture]
    public class DrinkTests : AdventTestFixture
    {
        [Test]
        public void cannot_drink_if_there_is_nothing_to_drink()
        {
            Location = Rooms.Get<BelowTheGrate>();
            IList<string> results = parser.Parse("drink water");
            Assert.AreEqual(L.CantSeeObject, results[0]);
        }

        [Test]
        public void can_drink_contained_liquids()
        {
            Location = Rooms.Get<BelowTheGrate>();

            Bottle bottle = Objects.Get<Bottle>() as Bottle;
            bottle.Add<WaterInTheBottle>();

            Inventory.Add(bottle);

            IList<string> results = parser.Parse("drink h2o");
            Assert.AreEqual("You drink the cool, refreshing water, draining the bottle in the process.", results[0]);
        }

        [Test]
        public void should_favor_inventory_over_location()
        {
            // there are two sources of water: the stream and the bottle in inventory
            Location = Rooms.Get<InsideBuilding>();

            Bottle bottle = Objects.Get<Bottle>() as Bottle;
            bottle.Add<WaterInTheBottle>();

            Inventory.Add(bottle);

            IList<string> results = parser.Parse("drink water");
            Assert.AreEqual("You drink the cool, refreshing water, draining the bottle in the process.", results[0]);
        }

        [Test]
        public void should_empty_bottle()
        {
            Bottle bottle = Objects.Get<Bottle>() as Bottle;
            bottle.Add<WaterInTheBottle>();

            Inventory.Add(bottle);

            Assert.AreEqual(1, bottle.Contents.Count);

            parser.Parse("drink water");
            
            Assert.AreEqual(0, bottle.Contents.Count);
        }

        [Test]
        public void should_drink_from_stream()
        {
            Location = Rooms.Get<InsideBuilding>();

            IList<string> results = parser.Parse("drink water");

            Assert.AreEqual("You have taken a drink from the stream. " +
                            "The water tastes strongly of minerals, but is not unpleasant. " +
                            "It is extremely cold.", results[0]);
        }

        [Test]
        public void cannot_drink_just_anything()
        {
            Location = Rooms.Get<InsideBuilding>();
            IList<string> results = parser.Parse("drink food");
            Assert.AreEqual("You can't drink that.", results[0]);
        }

        [Test]
        public void cannot_drink_items_that_dont_exist()
        {
            Location = Rooms.Get<InsideBuilding>();
            IList<string> results = parser.Parse("drink cola");
            Assert.AreEqual(L.CantSeeObject, results[0]);
        }
    }
}
