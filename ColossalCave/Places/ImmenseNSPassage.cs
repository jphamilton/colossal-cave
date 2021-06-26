using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Actions;
using ColossalCave.Things;

namespace ColossalCave.Places
{
    public class ImmenseNSPassage : BelowGround
    {
        public override void Initialize()
        {
            Name = "Immense N/S Passage";
            Synonyms.Are("immense", "n/s", "passage");
            Description = "You are at one end of an immense north/south passage.";

            SouthTo<GiantRoom>();

            NorthTo(() =>
            {
                var rustyDoor = Room<RustyDoor>();

                if (rustyDoor.Locked)
                {
                    Redirect<Open>(rustyDoor, v => v.Expects(rustyDoor));
                    return this;
                }

                if (!rustyDoor.Open)
                {
                    rustyDoor.Open = true;
                    Print("(first wrenching the door open)\n");
                }

                return rustyDoor;
            });

        }
    }

    public class RustyDoor : Door
    {
        public override void Initialize()
        {
            Name = "rusty door";
            Synonyms.Are("door", "hinge", "hinges", "massive", "rusty", "iron");
            Description = "It's just a big iron door.";
            Openable = false;
            Locked = true;

            FoundIn<ImmenseNSPassage>();

            WhenOpen = "The way north leads through a massive, rusty, iron door.";
            WhenClosed = "The way north is barred by a massive, rusty, iron door.";

            DoorTo(() => Room<CavernWithWaterfall>());

            DoorDirection(Direction<North>);

            Before<Open>(() =>
            {
                if (Locked)
                {
                    return Print("The hinges are quite thoroughly rusted now and won't budge.");
                }

                return false;
            });

            Before<Close>(() =>
            {
                if (Open)
                {
                    return "With all the effort it took to get the door open, I wouldn't suggest closing it again.";
                }

                return "No problem there -- it already is.";
            });

            Before<Oil>(() =>
            {
                var bottle = Get<Bottle>();
                var oilInBottle = Get<OilInTheBottle>();

                if (Inventory.Contains(bottle) && bottle.Contains(oilInBottle))
                {
                    oilInBottle.Remove();
                    Locked = false;
                    Openable = true;
                    Print("The oil has freed up the hinges so that the door will now move, although it requires some effort.");
                }
                else
                {
                    Print("You have nothing to oil it with.");
                }

                return true;
            });

            Before<Water>(() =>
            {
                var bottle = Get<Bottle>();
                var waterInBottle = Get<WaterInTheBottle>();

                if (Inventory.Contains(bottle) && bottle.Contains(waterInBottle))
                {
                    waterInBottle.Remove();
                    Locked = true;
                    Openable = false;
                    Print("The hinges are quite thoroughly rusted now and won't budge.");
                }
                else
                {
                    Print("You have nothing to water it with.");
                }

                return true;
            });

            After<Open>(() => "The door heaves open with a shower of rust.");
        }
    }
}


