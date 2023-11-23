
using Adventure.Net;
using Adventure.Net.Actions;
using Adventure.Net.Places;
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

        Before<ThrowAt>(() =>
        {
            if (Noun is Axe)
            {
                if (Random.Number(1, 3) != 1)
                {
                    Remove();
                    Noun.MoveToLocation();
                    Kill();
                    return Print("You killed a little dwarf! The body vanishes in a cloud of greasy black smoke.");
                }

                Noun.MoveToLocation();

                return Print("Missed! The little dwarf dodges out of the way of the axe.");
            }

            return Give();
        });

        Daemon = () =>
        {
            if (CurrentRoom.Is<Darkness>())
            {
                return;
            }

            if (Count == 0)
            {
                DaemonStarted = false;
                return;
            }

            var location = Location;

            if (location == null)
            {
                var room = CurrentRoom.Location;

                if (((BelowGround)room).NoDwarf || room.Light)
                {
                    return;
                }

                if (Random.Number(1, 100) <= Count)
                {
                    var bear = Get<Bear>();
                    var troll = Get<BurlyTroll>();

                    if (IsHere<Bear>() || IsHere<BurlyTroll>())
                    {
                        return;
                    }

                    Print("\n");

                    if (IsHere<Dragon>())
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

            if (location != CurrentRoom.Location)
            {
                if (location is Darkness)
                {
                    return;
                }

                if (((BelowGround)location).NoDwarf || location.Light)
                {
                    return;
                }

                if (Random.Number(1, 100) <= 96 && location is not MirrorCanyon)
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

                if (location is MirrorCanyon)
                {
                    Print("The dwarf admires himself in the mirror.");
                    return;
                }

                var throws = "The dwarf throws a nasty little knife at you, ";

                if (Random.Number(1, 1000) < 95)
                {
                    Print($"{throws} and hits!");
                    GameOver.Dead();
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
        if (Noun is TastyFood)
        {
            return Print("You fool, dwarves eat only coal! Now you've made him *really* mad!");
        }

        return Print("The dwarf is not at all interested in your offer. (The reason being, perhaps, that if he kills you he gets everything you have anyway.)");
    }
}
