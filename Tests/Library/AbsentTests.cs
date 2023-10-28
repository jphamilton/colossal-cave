using Adventure.Net;
using ColossalCave.Places;
using Xunit;

namespace Tests.Library;

public class ShoeHorn : Object
{
    public override void Initialize()
    {
        Name = "shoe horn";
        Description = "It is an ancient device that has seen much use.";
        Absent = true;

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

        shoehorn.Absent = false;

        Assert.True(ObjectMap.Contains(building, shoehorn));
        Assert.True(ObjectMap.Contains(road, shoehorn));

        shoehorn.Absent = true;

        Assert.False(ObjectMap.Contains(building, shoehorn));
        Assert.False(ObjectMap.Contains(road, shoehorn));

        shoehorn.Absent = false;

        Assert.True(ObjectMap.Contains(building, shoehorn));
        Assert.True(ObjectMap.Contains(road, shoehorn));
    }
}
