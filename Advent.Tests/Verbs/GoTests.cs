using Adventure.Net;
using ColossalCave.MyRooms;
using NUnit.Framework;

namespace Advent.Tests.Verbs
{
    [TestFixture]
    public class GoTests : AdventTestFixture
    {
        [Test]
        public void can_go_in()
        {
            Location = Rooms.Get<EndOfRoad>();
            parser.Parse("go in");
            Assert.AreEqual(Rooms.Get<InsideBuilding>(), Location);
        }
    }
}