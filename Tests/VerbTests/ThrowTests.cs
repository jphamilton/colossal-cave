using Adventure.Net;
using ColossalCave.Places;
using ColossalCave.Things;
using System.Linq;
using Xunit;

namespace Tests.VerbTests;


public class ThrowTests : BaseTestFixture
{
    [Fact]
    public void throw_axe_at_dwarf()
    {
        Location = Rooms.Get<Y2>();
        Location.Light = true;

        var dwarf = Objects.Get<Dwarf>();

        dwarf.MoveToLocation();
        dwarf.HasThrownAxe = true;

        var axe = Inventory.Add<Axe>();

        var outcomes = new string[]
        {
            "You killed a little dwarf! The body vanishes in a cloud of greasy black smoke.",
            "Missed! The little dwarf dodges out of the way of the axe."
        };

        // dwarf has Before<ThrowAt> routine

        var result = Execute("throw axe at dwarf");

        Assert.True(outcomes.Intersect(result.Output).Any());
    }

    [Fact]
    public void throw_something_futile()
    {
        Location = Rooms.Get<Y2>();
        Location.Light = true;

        Inventory.Add<Axe>();

        Execute("throw axe at y2");
        Assert.Contains("Futile.", ConsoleOut);
    }

    [Fact]
    public void throw_something_at_a_donkey()
    {
        Location = Rooms.Get<Y2>();
        Location.Light = true;

        Inventory.Add<Axe>();
        Objects.Get<Donkey>().MoveToLocation();

        Execute("throw axe at donkey");
        Assert.Contains("You lack the nerve when it comes to the crucial moment.", ConsoleOut);
    }
}
