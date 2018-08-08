using System.Collections.Generic;
using ColossalCave.MyObjects;
using ColossalCave.MyRooms;
using Adventure.Net;
using NUnit.Framework;

namespace Advent.Tests.Verbs
{
    [TestFixture]
    public class EmptyTests : AdventTestFixture
    {
        [Test]
        public void cannot_empty_things_that_arent_containers()
        {
            Location = Rooms.Get<InsideBuilding>();
            IList<string> results = parser.Parse("empty food");
            Assert.AreEqual("The tasty food can't contain things.", results[0]);
        }

        [Test]
        public void cannot_empty_something_that_is_already_empty()
        {
            Location = Rooms.Get<InsideBuilding>();
            IList<string> results = parser.Parse("empty bottle");
            Assert.AreEqual("The bottle is already empty!", results[0]);
        }

        [Test]
        public void cannot_empty_something_that_is_closed()
        {
            var cage = Objects.Get<WickerCage>() as Container;
            cage.Add<Bottle>();
            cage.IsOpen = false; 
            Inventory.Add(cage);

            IList<string> results = parser.Parse("empty cage");
            Assert.AreEqual("The wicker cage is closed.", results[0]);
        }
    }
}