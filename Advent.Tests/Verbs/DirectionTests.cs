using System.Collections.Generic;
using ColossalCave.MyRooms;
using Adventure.Net;
using NUnit.Framework;

namespace Advent.Tests.Verbs
{
    [TestFixture]
    public class DirectionTests : AdventTestFixture
    {
        [Test]
        public void can_travel_valid_directions()
        {
            Location = Rooms.Get<InsideBuilding>();
            parser.Parse("w");
            Assert.AreEqual(Room<EndOfRoad>(), Location);
            parser.Parse("s");
            Assert.AreEqual(Room<Valley>(), Location);
            parser.Parse("s");
            Assert.AreEqual(Room<SlitInStreambed>(), Location);
            parser.Parse("s");
            Assert.AreEqual(Room<OutsideGrate>(), Location);
            parser.Parse("go north");
            Assert.AreEqual(Room<SlitInStreambed>(), Location);
            parser.Parse("run north");
            Assert.AreEqual(Room<Valley>(), Location);

        }

        [Test]
        public void standard_cant_go_that_way()
        {
            IList<string> results = parser.Parse("nw");
            Assert.AreEqual("You can't go that way.", results[0]);
        }

        [Test]
        public void custom_cant_go_that_way()
        {
            Location = Rooms.Get<InsideBuilding>();
            IList<string> results = parser.Parse("nw");
            Assert.AreEqual("The stream flows out through a pair of 1 foot diameter sewer pipes. The only exit is to the west.", results[0]);
        }

        [Test]
        public void cannot_travel_through_closed_doors()
        {
            Location = Rooms.Get<OutsideGrate>();
            parser.Parse("d");
            Assert.AreEqual(Rooms.Get<OutsideGrate>(), Location);

            Location = Rooms.Get<BelowTheGrate>();
            parser.Parse("u");
            Assert.AreEqual(Rooms.Get<BelowTheGrate>(), Location);

        }
    }
}
