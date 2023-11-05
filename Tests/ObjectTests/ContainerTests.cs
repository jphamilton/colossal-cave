using Adventure.Net;
using ColossalCave.Things;
using Xunit;

namespace Tests.ObjectTests;


public class ContainerTests : BaseTestFixture
{
    [Fact]
    public void should_not_display_contents_when_not_transparent()
    {
        var cage = Objects.Get<WickerCage>();
        cage.Transparent = false;

        var lamp = Objects.Get<BrassLantern>();
        cage.Add(lamp);

        cage.Open = false;

        Inventory.Add(cage);

        Execute("i");

        // should not be displayed in inventory
        Assert.Contains("wicker cage (which is closed)", ConsoleOut);
        Assert.DoesNotContain("brass lantern", ConsoleOut);
    }

    [Fact]
    public void should_reveal_contents_when_not_transparent_and_opened()
    {
        var cage = Objects.Get<WickerCage>();
        cage.MoveToLocation();
        cage.Transparent = false;
        cage.Touched = true;

        var lamp = Objects.Get<BrassLantern>();
        cage.Add(lamp);

        var keys = Objects.Get<SetOfKeys>();
        cage.Add(keys);

        cage.Open = false;

        Execute("look");

        Assert.Contains("wicker cage (which is closed)", ConsoleOut);

        ClearOutput();

        Execute("open cage");

        Assert.Contains("You open the wicker cage, revealing a brass lantern and a set of keys.", ConsoleOut);
    }

    [Fact]
    public void should_show_contents_in_room()
    {
        var cage = Objects.Get<WickerCage>();
        cage.MoveToLocation();
        cage.Touched = true;

        var lamp = Objects.Get<BrassLantern>();
        cage.Add(lamp);

        var keys = Objects.Get<SetOfKeys>();
        cage.Add(keys);

        cage.Open = false;

        Execute("look");

        Assert.Contains("wicker cage (which contains a brass lantern and a set of keys)", ConsoleOut);
    }
}
