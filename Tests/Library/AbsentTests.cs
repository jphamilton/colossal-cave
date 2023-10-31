using Adventure.Net;
using ColossalCave.Places;
using ColossalCave.Things;
using Xunit;

namespace Tests.Library;

public class AbsentTests : BaseTestFixture
{
    [Fact]
    public void can_toggle_absent()
    {
        Location = Room<InsideBuilding>();
        var lamp = Objects.Get<BrassLantern>();

        lamp.Absent = true;

        Execute("take all");

        Assert.False(Inventory.Contains(lamp));

        var x = Inventory.Items;

        lamp.Absent = false;

        Execute("take all");

        Assert.True(Inventory.Contains(lamp));
    }
}
