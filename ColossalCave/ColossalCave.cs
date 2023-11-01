using Adventure.Net;
using ColossalCave.Actions;
using ColossalCave.Places;
using ColossalCave.Things;

namespace ColossalCave;

public class ColossalCaveStory : StoryBase
{
    public ColossalCaveStory()
    {
        Story = "ADVENTURE";
        CurrentScore = 0;
        PossibleScore = 350;
    }

    protected override void Start()
    {
        Output.Bold("ADVENTURE");

        Output.Print(
            "By Will Crowther (1976) and Don Woods (1977)\n" +
            "Reconstructed in three steps by:\n" +
            "Donald Ekman, David M. Baggett (1993) and Graham Nelson (1994)\n" +
            "Ported from Inform 6 to C# by J.P. Hamilton (2009)\n" +
            "[In memoriam Stephen Bishop (1820?-1857): GN]\n"
        );

        // just for showing up!
        Score.Add(36);

        var dwarf = Objects.Get<Dwarf>();
        dwarf.DaemonStarted = true;

        var pirate = Objects.Get<Pirate>();
        pirate.DaemonStarted = true;

        var closer = Objects.Get<CaveCloser>();
        closer.DaemonStarted = true;

        Output.PrintLine();

        Location = Rooms.Get<EndOfRoad>();

        // Testing
        //PartOne();
        //PartTwo();
        //PartThree();
    }

    // helpers for testing
    private void PartOne()
    {
        Location = Rooms.Get<TopOfSmallPit>();

        var lamp = Take<BrassLantern>();
        var cage = Take<WickerCage>();
        var bird = Take<LittleBird>();
        var bottle = Take<Bottle>();

        Take<TastyFood>();
        Take<SetOfKeys>();
        Take<BlackRod>();

        bottle.Add<WaterInTheBottle>();

        var grate = Rooms.Get<Grate>();
        grate.Locked = false;
        grate.Open = true;

        bird.Remove();
        cage.Add(bird);
        cage.Open = false;

        lamp.On = true;
        lamp.Light = true;
        lamp.DaemonStarted = true;
    }

    private void PartTwo()
    {
        Move<LargeGoldNugget, InsideBuilding>();
        Move<BarsOfSilver, InsideBuilding>();
        Move<PreciousJewelry, InsideBuilding>();

        var dwarf = Objects.Get<Dwarf>();
        dwarf.DaemonStarted = false;

        var pirate = Objects.Get<Pirate>();
        pirate.DaemonStarted = false;

        var snake = Objects.Get<Snake>();
        snake.Remove();

        var bird = Objects.Get<LittleBird>();
        var cage = Objects.Get<WickerCage>();
        cage.Open = true;
        cage.Remove(bird);
        bird.MoveTo<HallOfMtKing>();

        Location = Rooms.Get<EastBankOfFissure>();
    }

    private void PartThree()
    {
        Move<Diamonds, InsideBuilding>();
        Move<TreasureChest, InsideBuilding>();
        Move<VelvetPillow, InsideBuilding>();
        Move<MingVase, InsideBuilding>();
        Move<GoldenEggs, InsideBuilding>();
        Move<GlisteningPearl, InsideBuilding>();
        Move<JeweledTrident, InsideBuilding>();
        Move<EggSizedEmerald, InsideBuilding>();
        Move<PlatinumPyramid, InsideBuilding>();
        Move<GoldenChain, InsideBuilding>();
        Move<RareSpices, InsideBuilding>();
        Move<RareCoins, InsideBuilding>();
        Move<PersianRug, InsideBuilding>();

        var crystalBridge = Rooms.Get<CrystalBridge>();
        crystalBridge.Absent = false;

        var plantStickingUp = Objects.Get<PlantStickingUp>();
        plantStickingUp.Absent = true;

        var plant = Objects.Get<Plant>();
        plant.Height = PlantSize.Huge;

        Score.Add(25); // for making to Hall of Mists
        Score.Add(219);

        Global.TreasuresFound = 15;

        var timer = Objects.Get<EndgameTimer>();

        timer.TimeLeft = 2;

        Location = Rooms.Get<SwissCheeseRoom>();
    }

    private void CaveAlmostClosed()
    {
        var timer = Objects.Get<EndgameTimer>();
        timer.TimeLeft = 2;

        var lamp = Objects.Get<BrassLantern>();
        lamp.On = true;
        lamp.Light = true;
        Inventory.Add(lamp);

        Global.TreasuresFound = Global.MaxTreasures;
        Location = Rooms.Get<ShellRoom>();
    }

    private T Take<T>() where T : Object
    {
        var obj = Objects.Get<T>();
        obj.Remove();
        Inventory.Add(obj);
        return obj;
    }

    private void Move<T, R>()
        where T : Object
        where R : Room
    {
        var obj = Objects.Get<T>();
        obj.MoveTo<R>();
    }
}

