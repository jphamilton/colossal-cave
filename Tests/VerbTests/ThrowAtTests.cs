using Adventure.Net;
using ColossalCave.Places;
using ColossalCave.Things;
using System.Linq;
using Xunit;

namespace Tests.VerbTests;


public class ThrowAtTests : BaseTestFixture
{
    [Fact]
    public void throw_axe_at_dwarf()
    {
        Location = Rooms.Get<Y2>();
        Location.Light = true;

        var dwarf = Objects.Get<Dwarf>();

        dwarf.MoveToLocation();
        dwarf.HasThrownAxe = true;

        var axe = Objects.Get<Axe>();
        Inventory.Items.Add(axe);

        var outcomes = new string[]
        {
            "You killed a little dwarf! The body vanishes in a cloud of greasy black smoke.",
            "Missed! The little dwarf dodges out of the way of the axe."
        };

        // dwarf has Before<ThrowAt> routine

        var result = Execute("throw axe at dwarf");

        Assert.True(outcomes.Intersect(result.Output).Any());
    }
}
