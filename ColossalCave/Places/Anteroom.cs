
namespace ColossalCave.Places;

public class Anteroom : BelowGround
{
    public override void Initialize()
    {
        Name = "In Anteroom";
        Synonyms.Are("anteroom");
        Description =
            "You are in an anteroom leading to a large passage to the east. " +
            "Small passages go west and up. " +
            "The remnants of recent digging are evident.";

        UpTo<ComplexJunction>();
        WestTo<Bedquilt>();
        EastTo<WittsEnd>();
    }
}
