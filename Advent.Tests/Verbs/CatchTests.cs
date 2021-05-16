using ColossalCave.MyRooms;
using Adventure.Net;
using NUnit.Framework;
using Item = Adventure.Net.Item;

namespace Advent.Tests.Verbs
{
    [TestFixture]
    public class CatchTests : AdventTestFixture
    {
        [Test]
        public void cannot_catch_inanimate_objects()
        {
            Location = Rooms.Get<InsideBuilding>();
            var results = parser.Parse("catch bottle");
            Assert.AreEqual("You can only do that to something animate.", results[0]);
        }

        [Test]
        public void cant_catch_this()
        {
            Location = Rooms.Get<InsideBuilding>();
            
            var shark = new Shark();
            shark.Initialize();
            Items.Add(shark);
            Location.Objects.Add(shark);
            
            var results = parser.Parse("catch shark");
            Assert.AreEqual("You can't catch that.", results[0]);
        }
    }

    public class Shark : Item
    {
        public override void Initialize()
        {
            Name = "shark";
            IsAnimate = true;
        }
    }
}
