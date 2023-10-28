namespace ColossalCave.Places;

public class RecentCaveIn : BelowGround
{
    public override void Initialize()
    {
        Name = "Recent Cave-in";
        Description = "The passage here is blocked by a recent cave-in.";

        SouthTo<GiantRoom>();
    }
}
