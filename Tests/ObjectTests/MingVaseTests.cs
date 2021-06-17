using ColossalCave;
using ColossalCave.Things;
using Adventure.Net;
using Xunit;

namespace Tests.ObjectTests
{

    public class MingVaseTests : BaseTestFixture
    {
        private Treasure vase;

        public MingVaseTests()
        {
            vase = Objects.Get<MingVase>();
            Inventory.Add(vase);
        }

        [Fact]
        public void should_break()
        {
            Execute("drop vase");
            Assert.Contains("The ming vase drops with a delicate crash.", Line(1));
        }

        [Fact]
        public void vase_should_not_exist()
        {
            Execute("drop vase");
            Assert.False(Inventory.Contains(vase));
            Assert.False(CurrentRoom.Has<MingVase>());
        }

        [Fact]
        public void should_not_break()
        {
            var pillow = Objects.Get<VelvetPillow>();
            pillow.MoveToLocation();

            var result = Execute("drop vase");
            Assert.Contains("(coming to rest, delicately, on the velvet pillow)", ConsoleOut);
        }

        [Fact]
        public void can_attack_the_vase()
        {
            Execute("hit vase");
            Assert.Contains("You have taken the vase and hurled it delicately to the ground.", Line(1));
        }

        [Fact]
        public void cannot_fill_vase()
        {
            var bottle = Objects.Get<Bottle>();
            Inventory.Add(bottle);
            Execute("put bottle in vase");
            Assert.Contains("The vase is too fragile to use as a container.", Line(1));
        }
    }
}
