using Adventure.Net;
using ColossalCave.MyRooms;
using NUnit.Framework;

namespace Advent.Tests.RoomTests
{
    [TestFixture]
    public class EndOfRoadTests : AdventTestFixture
    {
        protected override void OnSetUp()
        {
            Location = Rooms.Get<EndOfRoad>();
        }

        [Test]
        public void can_go_west()
        {
            parser.Parse("west");
            Assert.AreEqual(Rooms.Get<HillInRoad>(), Location);
        }

        [Test]
        public void can_go_east()
        {
            parser.Parse("east");
            Assert.AreEqual(Rooms.Get<InsideBuilding>(), Location);
        }

        [Test]
        public void can_go_down()
        {
            parser.Parse("down");
            Assert.AreEqual(Rooms.Get<Valley>(), Location);
        }

        [Test]
        public void can_go_south()
        {
            parser.Parse("south");
            Assert.AreEqual(Rooms.Get<Valley>(), Location);
        }

        [Test]
        public void can_go_north()
        {
            parser.Parse("north");
            Assert.AreEqual(Rooms.Get<Forest1>(), Location);
        }

        [Test]
        public void can_go_in()
        {
            parser.Parse("in");
            Assert.AreEqual(Rooms.Get<InsideBuilding>(), Location);
        }

        [Test]
        public void can_enter()
        {
            parser.Parse("enter");
            Assert.AreEqual(Rooms.Get<InsideBuilding>(), Location);
        }
    }
}