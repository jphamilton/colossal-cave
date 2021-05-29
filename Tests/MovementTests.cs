using Adventure.Net;
using ColossalCave.Places;
using Xunit;

namespace Tests
{

    public class MovementTests : BaseTestFixture
    {
        public MovementTests()
        {
            Location = Room<EndOfRoad>();
        }

        [Fact]
        public void can_go_west()
        {
            Execute("west");
            Assert.Equal(Room<HillInRoad>(), Location);
        }

        [Fact]
        public void can_go_east()
        {
            Execute("east");
            Assert.Equal(Room<InsideBuilding>(), Location);
        }

        [Fact]
        public void can_go_down()
        {
            Execute("down");
            Assert.Equal(Room<Valley>(), Location);
        }

        [Fact]
        public void can_go_south()
        {
            Execute("south");
            Assert.Equal(Room<Valley>(), Location);
        }

        [Fact]
        public void can_go_north()
        {
            Execute("north");
            Assert.Equal(Room<Forest1>(), Location);
        }

        [Fact]
        public void can_go_in()
        {
            Execute("in");
            Assert.Equal(Room<InsideBuilding>(), Location);
        }

        [Fact]
        public void can_enter()
        {
            Execute("enter");
            Assert.Equal(Room<InsideBuilding>(), Location);
        }
    }
}