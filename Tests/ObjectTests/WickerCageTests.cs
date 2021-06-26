using Adventure.Net;
using ColossalCave.Things;
using Xunit;

namespace Tests.ObjectTests
{

    public class WickerCageTests : BaseTestFixture
    {
        [Fact]
        public void bird_not_in_cage()
        {
            var cage = Objects.Get<WickerCage>();
            var bird = Objects.Get<LittleBird>();

            bird.MoveToLocation();

            cage.Open = false;
            Inventory.Add(cage);

            Execute("release bird");
            Assert.Equal("The bird is not caged now.", Line1);

            Assert.False(cage.Contains<LittleBird>());
            Assert.False(cage.Open);
        }

        [Fact]
        public void bird_should_fly_out()
        {
            var bird = Objects.Get<LittleBird>();
            var cage = Objects.Get<WickerCage>();

            cage.Add(bird);
            cage.Open = false;
            Inventory.Add(cage);

            Execute("open cage");

            Assert.Equal("(releasing the little bird)", Line1);
            Assert.Equal("The little bird flies free.", Line2);

            Assert.DoesNotContain("You can't release that.", ConsoleOut);
            Assert.False(cage.Contains<LittleBird>());
            Assert.True(cage.Open);
            Assert.True(bird.InRoom);

        }


    }
}
