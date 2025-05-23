
using Adventure.Net;
using Adventure.Net.ActionRoutines;
using Adventure.Net.Places;
using Adventure.Net.Things;
using Adventure.Net.Utilities;
using ColossalCave.Actions;
using ColossalCave.Places;

namespace ColossalCave.Things;

public class Dwarf : Object
{
    public int Count { get; set; } = 5;
    public bool HasThrownAxe { get; set; }

    public void Kill()
    {
        if (Count > 0)
        {
            Count--;
        }
    }

    public override void Initialize()
    {
        Name = "threatening little dwarf";
        Synonyms.Are("dwarf", "threatening", "nasty", "little", "mean");
        Description = "It's probably not a good idea to get too close. Suffice it to say the little guy's pretty aggressive.";
        InitialDescription = "A threatening little dwarf hides in the shadows.";
        Animate = true;

        Before<Attack>(() => "Not with your bare hands. No way.");

        Before<Kick>(() => "You boot the dwarf across the room. He curses, then gets up and brushes himself off. Now he's madder than ever!");

        Before<Give>(Give);

        Before<Throw>(() =>
        {
            if (First is Axe)
            {
                if (Random.Number(1, 3) != 1)
                {
                    Remove();
                    First.MoveToLocation();
                    Kill();
                    return Print("You killed a little dwarf! The body vanishes in a cloud of greasy black smoke.");
                }

                First.MoveToLocation();

                return Print("Missed! The little dwarf dodges out of the way of the axe.");
            }

            return Give();
        });

        Daemon = () =>
        {
            if (Player.Location is Darkness || Player.Location is not BelowGround room)
            {
                return;
            }

            if (Count == 0)
            {
                DaemonRunning = false;
                return;
            }

            var dwarfLocation = Location;

            if (dwarfLocation == null)
            {
                //var room = Player.Location;

                if (room.NoDwarf || room.Light)
                {
                    return;
                }

                if (Random.Number(1, 100) <= Count)
                {
                    var bear = Get<Bear>();
                    var troll = Get<BurlyTroll>();

                    if (bear.InRoom || troll.InRoom)
                    {
                        return;
                    }

                    Print("\n");

                    var dragon = Get<Dragon>();

                    if (dragon.InRoom)
                    {
                        Count--;
                        Print("A dwarf appears, but with one casual blast the dragon vapourises him!");
                        return;
                    }

                    MoveToLocation();
                    Print("A threatening little dwarf comes out of the shadows!");
                }

                return;
            }

            if (dwarfLocation != Player.Location)
            {
                if (dwarfLocation is Darkness)
                {
                    return;
                }

                if (((BelowGround)dwarfLocation).NoDwarf || dwarfLocation.Light)
                {
                    return;
                }

                if (Random.Number(1, 100) <= 96 && dwarfLocation is not MirrorCanyon)
                {
                    MoveToLocation();
                    Print("\nThe dwarf stalks after you...\n");
                }
                else
                {
                    Remove();
                }

                return;
            }

            if (Random.Number(1, 100) < 75)
            {
                Print("\n");

                if (!HasThrownAxe)
                {
                    var axe = Get<Axe>();
                    HasThrownAxe = true;
                    axe.MoveToLocation();
                    Remove();
                    Print("The dwarf throws a nasty little axe at you, misses, curses, and runs away.");
                    return;
                }

                if (dwarfLocation is MirrorCanyon)
                {
                    Print("The dwarf admires himself in the mirror.");
                    return;
                }

                var throws = "The dwarf throws a nasty little knife at you, ";

                if (Random.Number(1, 1000) < 95)
                {
                    Print($"{throws} and hits!");
                    Dead();
                    return;
                }

                Print($"{throws} but misses!");
                return;
            }

            if (Random.Number(1, 3) == 1)
            {
                Remove();
                Print("\nTiring of this, the dwarf slips away.");
            }
        };
    }

    private bool Give()
    {
        if (First is TastyFood)
        {
            return Print("You fool, dwarves eat only coal! Now you've made him *really* mad!");
        }

        return Print("The dwarf is not at all interested in your offer. (The reason being, perhaps, that if he kills you he gets everything you have anyway.)");
    }
}
