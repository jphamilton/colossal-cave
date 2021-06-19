using Adventure.Net;
using ColossalCave.Places;
using Xunit;

namespace Tests.Library
{
    public class ShoeHorn : Item
    {
        public override void Initialize()
        {
            Name = "shoe horn";
            Description = "It is an ancient device that has seen much use.";
            IsAbsent = true;

            FoundIn<InsideBuilding, EndOfRoad>();

        }
        
    }

    public class AbsentTests : BaseTestFixture
    {
        [Fact]
        public void can_toggle_isAbsent()
        {
            var building = Room<InsideBuilding>();
            var road = Room<EndOfRoad>();

            var shoehorn = new ShoeHorn();
            shoehorn.Initialize();

            Assert.False(ObjectMap.Contains(building, shoehorn));
            Assert.False(ObjectMap.Contains(road, shoehorn));

            shoehorn.IsAbsent = false;

            Assert.True(ObjectMap.Contains(building, shoehorn));
            Assert.True(ObjectMap.Contains(road, shoehorn));

            shoehorn.IsAbsent = true;

            Assert.False(ObjectMap.Contains(building, shoehorn));
            Assert.False(ObjectMap.Contains(road, shoehorn));

            shoehorn.IsAbsent = false;

            Assert.True(ObjectMap.Contains(building, shoehorn));
            Assert.True(ObjectMap.Contains(road, shoehorn));
        }
    }
}
