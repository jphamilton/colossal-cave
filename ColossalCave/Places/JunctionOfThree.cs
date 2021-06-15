namespace ColossalCave.Places
{
    public class JunctionOfThree : BelowGround
    {
        public override void Initialize()
        {
            Name = "Junction of Three Secret Canyons";
            Synonyms.Are("junction", "of", "three", "secret", "canyons");
            Description = "You are in a secret canyon at a junction of three canyons, bearing north, south, and se. " +
             "The north one is as tall as the other two combined.";

            SouthEastTo<Bedquilt>();
            SouthTo<SecretNSCanyon1>();
            NorthTo<WindowOnPit2>();
        }
    }
}
