namespace ColossalCave.Places;

public class SecretNSCanyon1 : BelowGround
{
    public override void Initialize()
    {
        Name = "Secret N/S Canyon";
        Synonyms.Are("secret", "n/s", "canyon", "1");
        Description = "You are in a secret N/S canyon above a sizable passage.";

        NorthTo<JunctionOfThree>();
        DownTo<Bedquilt>();
        SouthTo<AtopStalactite>();
    }
}
