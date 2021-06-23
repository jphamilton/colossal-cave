using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Things;

namespace ColossalCave.Places
{
    public class SoftRoom : BelowGround
    {
        public override void Initialize()
        {
            Name = "In Soft Room";
            Synonyms.Are("soft", "room");
            Description =
                "You are in the soft room. " +
                "The walls are covered with heavy curtains, the floor with a thick pile carpet. " +
                "Moss covers the ceiling.";

            WestTo<SwissCheeseRoom>();
        }
    }

    public class VelvetPillow : Item
    {
        public override void Initialize()
        {
            Name = "velvet pillow";
            Synonyms.Are("pillow", "velvet", "small");
            Description = "It's just a small velvet pillow.";
            InitialDescription = "A small velvet pillow lies on the floor.";

            FoundIn<SoftRoom>();
        }
    }

    public class Carpet : Scenic
    {
        public override void Initialize()
        {
            Name = "carpet";
            Synonyms.Are("carpet", "shag", "pile", "heavy", "thick");
            Description = "The carpet is quite plush.";
            FoundIn<SoftRoom>();
        }
    }

    public class Curtains : Scenic
    {
        public override void Initialize()
        {
            Name = "curtains";
            Synonyms.Are("curtain", "curtains", "heavy", "thick");
            Description = "They seem to absorb sound very well.";

            FoundIn<SoftRoom>();

            
            Before<Take>(() =>
            {
                Print("Now don't go ripping up the place!");
                return true;
            });

            Before<LookUnder>(NothingExciting);
            Before<Search>(NothingExciting);

        }

        private bool NothingExciting()
        {
            Print("You don't find anything exciting behind the curtains.");
            return true;
        }
    }

    public class Moss : Scenic
    {
        public override void Initialize()
        {
            Name = "moss";
            Synonyms.Are("moss", "typical", "everyday");
            Description = "It just looks like your typical, everyday moss.";
            Edible = true;

            FoundIn<SoftRoom>();

            Before<Take>(() =>
            {
                Print("It crumbles to nothing in your hands.");
                return true;
            });
        }
    }

}


