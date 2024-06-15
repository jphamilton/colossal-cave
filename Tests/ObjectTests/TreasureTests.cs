using Adventure.Net;
using ColossalCave.Places;
using ColossalCave.Things;
using ColossalCave;
using System.Linq;
using Xunit;
using ColossalCave.Actions;

namespace Tests.ObjectTests;

public class TreasureTests : BaseTestFixture
{
    [Fact]
    public void should_save_treasure_stats()
    {
        Location = Rooms.Get<EndOfRoad>();

        var treasures = Objects.All.Where(x => x.GetType().IsSubclassOf(typeof(Treasure))).ToList();
        foreach (var treasure in treasures)
        {
            treasure.MoveToLocation();
            treasure.Locked = false;

            var command = $"take {treasure.Synonyms[0]}";
            Execute($"{command}");
            Assert.Contains(treasure, Inventory.Items);
            var x = ConsoleOut;
            Execute("e");
            Execute($"drop {treasure.Synonyms[0]}");
            Execute("w");
            var y = ConsoleOut;
        }

        var score = Context.Story.CurrentScore;
        Assert.Equal(15, Global.State.TreasuresFound);

    }

    [Fact]
    public void emerald_should_trigger_treasure_after_take()
    {
        var emerald = Objects.Get<EggSizedEmerald>();
        emerald.MoveToLocation();
        Execute("take emerald");
    }

}
