using ColossalCave.Places;
using Xunit;

namespace Tests.Verbs
{
    public class DirectionTests : BaseTestFixture
    {
        [Fact]
        public void can_go()
        {
            Execute("go west");
            Assert.Equal(Room<EndOfRoad>(), Location);
        }

        [Fact]
        public void can_travel_valid_directions()
        {
            Execute("w");
            Assert.Equal(Room<EndOfRoad>(), Location);
            Execute("s");
            Assert.Equal(Room<Valley>(), Location);
            Execute("s");
            Assert.Equal(Room<SlitInStreambed>(), Location);
            Execute("s");
            Assert.Equal(Room<OutsideGrate>(), Location);
            Execute("go north");
            Assert.Equal(Room<SlitInStreambed>(), Location);
            Execute("run north");
            Assert.Equal(Room<Valley>(), Location);
        }

        [Fact]
        public void standard_cant_go_that_way()
        {
            Location = Room<EndOfRoad>();
            var result = Execute("sw");
            Assert.Equal("You can't go that way.", Line(1));
        }

        [Fact]
        public void custom_cant_go_that_way()
        {
            Execute("nw");
            Assert.Equal("The stream flows out through a pair of 1 foot diameter sewer pipes. The only exit is to the west.", Line(1));
        }

        [Fact]
        public void cannot_travel_through_closed_doors()
        {
            Location = Room<OutsideGrate>();
            Execute("d");
            Assert.Equal(Room<OutsideGrate>(), Location);

            Location = Room<BelowTheGrate>();
            Execute("u");
            Assert.Equal(Room<BelowTheGrate>(), Location);

        }
    }
}
