using ColossalCave.Places;
using Adventure.Net;
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
            CurrentRoom.Objects.Add(bird);

            cage.IsOpen = false;
            Inventory.Add(cage);

            Execute("release bird");
            Assert.Equal("The bird is not caged now.", Line(1));

            Assert.False(cage.Contains<LittleBird>());
            Assert.False(cage.IsOpen);
        }

        [Fact]
        public void bird_should_fly_out()
        {
            var bird = Objects.Get<LittleBird>();
            var cage = Objects.Get<WickerCage>();

            cage.Add(bird);
            cage.IsOpen = false;
            Inventory.Add(cage);

            Execute("open cage");

            Assert.Equal("(releasing the little bird)", Line(1));
            Assert.Equal("You open the wicker cage.", Line(2));
            Assert.Equal("The little bird flies free.", Line(3));

            Assert.DoesNotContain("You can't release that.", ConsoleOut);
            Assert.False(cage.Contains<LittleBird>());
            Assert.True(cage.IsOpen);
            Assert.True(bird.InRoom);

        }


    }
}
