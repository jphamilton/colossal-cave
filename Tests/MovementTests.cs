using Adventure.Net;
using ColossalCave.Places;
using Xunit;

namespace Tests
{

    public class MovementTests : BaseTestFixture
    {
        public MovementTests()
        {
            Room = Rooms.Get<EndOfRoad>();
        }

        [Fact]
        public void can_go_west()
        {
            Execute("west");
            Assert.Equal(Rooms.Get<HillInRoad>(), Room);
        }

        [Fact]
        public void can_go_east()
        {
            Execute("east");
            Assert.Equal(Rooms.Get<InsideBuilding>(), Room);
        }

        [Fact]
        public void can_go_down()
        {
            Execute("down");
            Assert.Equal(Rooms.Get<Valley>(), Room);
        }

        [Fact]
        public void can_go_south()
        {
            Execute("south");
            Assert.Equal(Rooms.Get<Valley>(), Room);
        }

        [Fact]
        public void can_go_north()
        {
            Execute("north");
            Assert.Equal(Rooms.Get<Forest1>(), Room);
        }

        [Fact]
        public void can_go_in()
        {
            Execute("in");
            Assert.Equal(Rooms.Get<InsideBuilding>(), Room);
        }

        [Fact]
        public void can_enter()
        {
            Execute("enter");
            Assert.Equal(Rooms.Get<InsideBuilding>(), Room);
        }
    }
}