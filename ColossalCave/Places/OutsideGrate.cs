using Adventure.Net.ActionRoutines;
using ColossalCave.Things;

namespace ColossalCave.Places;

public class OutsideGrate : AboveGround
{
    public override void Initialize()
    {
        Name = "Outside Grate";
        Synonyms.Are("outside", "grate");

        Description = "You are in a 20-foot depression floored with bare dirt. " +
                      "Set into the dirt is a strong steel grate mounted in concrete. " +
                      "A dry streambed leads into the depression.";

        EastTo<Forest1>();
        WestTo<Forest1>();
        SouthTo<Forest1>();
        NorthTo<SlitInStreambed>();

        DownTo(() =>
        {
            Grate grate = Room<Grate>();

            if (!grate.Locked && !grate.Open)
            {
                Implicit.Action<Open>(grate);
            }

            return grate;
        });

    }
}

public class TwentyFootDepression : Scenic
{
    public override void Initialize()
    {
        Name = "20-foot depression";
        Synonyms.Are("depression", "dirt", "twenty", "foot", "bare", "20-foot");
        Description = "You're standing in it.";

        FoundIn<OutsideGrate>();
    }
}

