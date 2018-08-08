using System.Collections.Generic;
using Adventure.Net;
using ColossalCave.MyRooms;
using NUnit.Framework;

namespace Advent.Tests.RoomTests
{
    public class HillInRoadTests : AdventTestFixture
    {
        protected override void OnSetUp()
        {
            Location = Rooms.Get<HillInRoad>();
        }
        
        [Test]
        public void cannot_enter_well_house_if_not_visited()
        {
            var results = parser.Parse("enter wellhouse");
            Assert.AreEqual("It's too far away.", results[0]);
        }

        [Test]
        public void can_enter_well_house_if_visited()
        {
            var wellHouse = Rooms.Get<InsideBuilding>();
            wellHouse.Visited = true;

            parser.Parse("enter wellhouse");
            Assert.AreEqual(wellHouse, Location);
        }

        [Test]
        public void can_go_east()
        {
            parser.Parse("e");
            Assert.AreEqual(Rooms.Get<EndOfRoad>(), Location);
        }

        [Test]
        public void can_go_north()
        {
            parser.Parse("n");
            Assert.AreEqual(Rooms.Get<EndOfRoad>(), Location);
        }

        [Test]
        public void can_go_down()
        {
            parser.Parse("d");
            Assert.AreEqual(Rooms.Get<EndOfRoad>(), Location);
        }

        [Test]
        public void can_go_south()
        {
            parser.Parse("s");
            Assert.AreEqual(Rooms.Get<Forest1>(), Location);
        }

        [Test]
        public void cant_go_that_way()
        {
            var valid = new List<string> {"e", "east", "n", "north", "d", "down", "s", "south"};
            Compass.Directions.Remove(valid);

            foreach (var dir in Compass.Directions)
            {
                Location = Rooms.Get<HillInRoad>();
                var results = parser.Parse(dir);
                Assert.AreEqual("You can't go that way.", results[0]);
            }
        }
    }
}