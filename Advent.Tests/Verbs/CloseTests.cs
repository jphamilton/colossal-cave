using System.Collections.Generic;
using ColossalCave.MyRooms;
using Adventure.Net;
using NUnit.Framework;

namespace Advent.Tests.Verbs
{
    [TestFixture]
    public class CloseTests : AdventTestFixture
    {
        [Test]
        public void cannot_close_things_that_are_not_meant_to_be_closed()
        {
            Location = Rooms.Get<InsideBuilding>();
            IList<string> results = parser.Parse("close bottle");
            Assert.AreEqual("That's not something you can close.", results[0]);
        }

        [Test]
        public void cannot_close_things_that_are_already_closed()
        {
            Location = Rooms.Get<OutsideGrate>();
            IList<string> results = parser.Parse("close grate");
            Assert.AreEqual("That's already closed.", results[0]);
        }

        [Test]
        public void can_close()
        {
            Location = Rooms.Get<OutsideGrate>();
            Door grate = Rooms.Get<Grate>() as Door;
            grate.IsOpen = true;
            IList<string> results = parser.Parse("close grate");
            Assert.AreEqual("You close the steel grate.", results[0]);
        }
    }
}
