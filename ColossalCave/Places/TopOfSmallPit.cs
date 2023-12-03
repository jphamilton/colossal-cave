using Adventure.Net;
using Adventure.Net.Actions;
using Adventure.Net.Things;
using ColossalCave.Things;

namespace ColossalCave.Places;

public class TopOfSmallPit : BelowGround
{
    public override void Initialize()
    {
        Name = "At Top of Small Pit";

        Synonyms.Are("top", "of", "small", "pit");

        Description =
            "At your feet is a small pit breathing traces of white mist. " +
            "A west passage ends here except for a small crack leading on.\n\n" +
            "Rough stone steps lead down the pit.";

        NoDwarf = true;

        EastTo<BirdChamber>();

        WestTo(() =>
            {
                Print("That crack is far too small for you to follow.");
                return this;
            });

        DownTo(() =>
            {
                if (Player.IsCarrying<LargeGoldNugget>())
                {
                    Print("You are at the bottom of the pit with a broken neck.");
                    GameOver.Dead();
                    return this;
                }

                return Rooms.Get<HallOfMists>();

            });

    }
}

#region Scenery

public class PitCrack : Scenic
{
    public override void Initialize()
    {
        Name = "crack";
        Synonyms.Are("crack", "small");
        Description = "The crack is very small -- far too small for you to follow.";

        FoundIn<TopOfSmallPit>();

        Before<Enter>(() =>
        {
            Print("The crack is far too small for you to follow.");
            return true;
        });
    }
}

public class SmallPit : Scenic
{
    public override void Initialize()
    {
        Name = "small pit";
        Synonyms.Are("pit", "small");
        Description = "The pit is breathing traces of white mist.";

        FoundIn<TopOfSmallPit>();
    }
}

#endregion
