using Adventure.Net;
using Adventure.Net.Actions;

namespace ColossalCave.Places;

public class SwEnd : BelowGround
{
    public override void Initialize()
    {
        Name = "SW End of Repository";

        Synonyms.Are("southwest", "sw", "end", "of", "repository");

        Description =
            "You are at the southwest end of the repository. " +
            "To one side is a pit full of fierce green snakes. " +
            "On the other side is a row of small wicker cages, each of which contains a little sulking bird. " +
            "In one corner is a bundle of black rods with rusty marks on their ends. " +
            "A large number of velvet pillows are scattered about on the floor. " +
            "A vast mirror stretches off to the northeast. " +
            "At your feet is a large steel grate, next to which is a sign which reads, " +
            "\"TREASURE VAULT. Keys in main office.\"";

        Light = true;

        NorthEastTo<NeEnd>();

        DownTo<RepositoryGrate>();
    }
}

public class RepositoryGrate : Door
{
    public RepositoryGrate()
    {
        Locked = true;
    }

    public override void Initialize()
    {
        Name = "steel grate";
        Synonyms.Are("ordinary", "steel", "grate", "grating");
        Description = "It just looks like an ordinary steel grate.";

        FoundIn<SwEnd>();

        Describe = () =>
        {
            if (Open)
            {
                return "The grate is open.";
            }

            return "The grate is closed.";
        };

        DoorDirection(Direction<Down>);

        DoorTo(() => Room<OutsideGrate>());
    }
}

public class BlackMarkRod : Object
{
    public override void Initialize()
    {
        Name = "black rod with a rusty mark on the end";
        Synonyms.Are("rod", "black", "rusty", "mark", "three", "foot", "iron", "explosive", "dynamite", "blast");
        Description = "It's a three foot black rod with a rusty mark on an end.";
        InitialDescription = "A three foot black rod with a rusty mark on one end lies nearby.";

        FoundIn<SwEnd>();

        Before<Wave>(() => "Nothing happens.");
    }
}
