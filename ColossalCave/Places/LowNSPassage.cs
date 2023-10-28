namespace ColossalCave.Places;

public class LowNSPassage : BelowGround
{
    public override void Initialize()
    {
        Name = "Low N/S Passage";
        Synonyms.Are("low", "n/s", "passage");
        Description = "You are in a low N/S passage at a hole in the floor. The hole goes down to an E/W passage.";

        SouthTo<HallOfMtKing>();

        DownTo<DirtyPassage>();

        NorthTo<Y2>();
    }
}

