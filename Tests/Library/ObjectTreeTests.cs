using Adventure.Net;
using Adventure.Net.Things;
using ColossalCave.Places;
using ColossalCave.Things;
using System.Linq;
using Xunit;

namespace Tests.Library;

public class ObjectTreeTests : BaseTestFixture
{
    [Fact]
    public void filter_should_return_objects_providing_light()
    {
        var tree = new ObjectTree(Player.Location, false);
        var lit = tree.Any(x => x.Light);

        Assert.True(lit);
    }

    [Fact]
    public void noodling()
    {
        var invisibleDonkey = Objects.Get<Donkey>();
        invisibleDonkey.MoveToLocation();
        invisibleDonkey.Absent = true;

        var box = Objects.Get<OpaqueBox>();
        box.MoveToLocation();

        var lamp = Objects.Get<BrassLantern>();

        var keys = Objects.Get<SetOfKeys>();
        var food = Objects.Get<TastyFood>();
        var bottle = Objects.Get<Bottle>();
        var table = Objects.Get<Table>();

        table.MoveToLocation();
        table.Add(bottle);

        box.Add(food);
        box.Add(keys);
        box.Open = false;

        Inventory.Add(box);

        var tree = new ObjectTree(Location, false);

        var found = tree.GetObjects(out bool lit);

        Assert.True(lit);

        var inScope = CurrentRoom.ObjectsInScope(false);
        var missing = inScope.Where(x => !found.Contains(x)).ToList();

        Assert.Contains(bottle, found);
        Assert.DoesNotContain(invisibleDonkey, found);
        Assert.DoesNotContain(food, found);
        Assert.DoesNotContain(keys, found);

        tree = new ObjectTree(Location, true);
        found = tree.GetObjects();

        Assert.Contains(invisibleDonkey, found);

        MovePlayer.To<EndOfRoad>();

        // contents now visible
        box.Open = true;

        tree = new ObjectTree(Location);

        found = tree.GetObjects();

        Assert.Contains(keys, found);
        Assert.Contains(food, found);
    }


}
