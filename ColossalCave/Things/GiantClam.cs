using Adventure.Net;
using Adventure.Net.ActionRoutines;
using Adventure.Net.Things;
using ColossalCave.Places;

namespace ColossalCave.Things;

public class GiantClam : Object
{
    public bool HasBeenOpened { get; set; }

    public override void Initialize()
    {
        Name = "giant clam";
        Synonyms.Are("giant", "clam", "oyster", "bivalve");

        Describe = () =>
        {
            if (HasBeenOpened)
            {
                return "There is an enormous oyster here with its shell tightly closed.";
            }

            return "There is an enormous clam here with its shell tightly closed.";
        };

        FoundIn<ShellRoom>();

        Before<Attack>(() => "The shell is very strong and is impervious to attack.");

        Before<Open>(() =>
        {
            //"You aren't strong enough to open the clam with your bare hands."
            var trident = Get<JeweledTrident>();
            if (!Inventory.Contains(trident))
            {
                return Print("You aren't strong enough to open the clam with your bare hands.");
            }

            return false;
        });

        Before<Examine>(() =>
        {
            if (Player.Location is NeEnd || Player.Location is SwEnd)
            {
                return
                    "Interesting. " +
                    "There seems to be something written on the underside of the oyster: " +
                    "\n\n " +
                    "\"There is something strange about this place, " +
                    "such that one of the curses I've always known now has a new effect.\"";
            }

            return "A giant bivalve of some kind.";
        });

        Before<Unlock>(() =>
        {
            if (Second is not JeweledTrident)
            {
                return Print($"The {Second.Name} isn't strong enough to open the clam.");
            }

            if (!HasBeenOpened)
            {
                Print("The oyster creaks open, revealing nothing but oyster inside. It promptly snaps shut again.");

                HasBeenOpened = true;

                Move<GlisteningPearl>.To<CulDeSac>();

                Print(
                    "\nA glistening pearl falls out of the clam and rolls away. " +
                    "Goodness, this must really be an oyster. " +
                    "(I never was very good at identifying bivalves.) " +
                    "Whatever it is, it has now snapped shut again."
                );

                return true;
            }

            return false;
        });
    }
}
