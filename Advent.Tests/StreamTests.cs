using System;
using System.Collections.Generic;
using ColossalCave.MyObjects;
using ColossalCave.MyRooms;
using Adventure.Net;
using NUnit.Framework;
using Object=Adventure.Net.Object;

namespace Advent.Tests
{
    [TestFixture]
    public class StreamTests : AdventTestFixture
    {
        protected override void OnSetUp()
        {
            Context.Story.Location = Rooms.Get<InsideBuilding>();
        }

        [Test]
        public void should_handle_BeforeTake()
        {
            Container bottle = Objects.Get<Bottle>() as Container;
            Object water = Objects.Get<WaterInTheBottle>();

            IList<string> results = parser.Parse("take bottle");

            results = parser.Parse("take stream");
            Assert.AreEqual("The bottle is now full of water.", results[0]);

            Assert.IsTrue(Inventory.Contains(bottle));
            Assert.IsTrue(bottle.Contents.Contains(water));

        }

        
    }
}
