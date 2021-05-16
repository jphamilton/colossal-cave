using ColossalCave.Objects;
using Adventure.Net;
using NUnit.Framework;

namespace Advent.Tests.ObjectTests
{
    [TestFixture]
    public class WickerCageTests : AdventTestFixture
    {
        [Test]
        public void bird_not_in_cage()
        {
            var cage = Items.Get<WickerCage>();
            var bird = Items.Get<LittleBird>();
            Location.Objects.Add(bird);

            cage.IsOpen = false;
            Inventory.Add(cage);

            var results = parser.Parse("release bird");
            Assert.AreEqual("The bird is not caged now.", results[0]);

            Assert.IsFalse(cage.Contains<LittleBird>());
            Assert.IsFalse(cage.IsOpen);
        }

        [Test]
        public void bird_should_fly_out()
        {
            var bird = Items.Get<LittleBird>();
            var cage = Items.Get<WickerCage>();

            cage.Add(bird);
            cage.IsOpen = false;
            Inventory.Add(cage);

            var results = parser.Parse("open cage");

            Assert.AreEqual("You open the wicker cage.", results[0]);
            Assert.AreEqual("(releasing the little bird)", results[1]);
            Assert.AreEqual("The little bird flies free.", results[2]);
            
            Assert.IsFalse(results.Contains("You can't release that."));
            Assert.IsFalse(cage.Contains<LittleBird>());
            Assert.IsTrue(cage.IsOpen);
            Assert.IsTrue(bird.AtLocation);

        }

      
    }
}
