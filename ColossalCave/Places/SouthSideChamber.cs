namespace ColossalCave.Places
{
    public class SouthSideChamber : BelowGround
    {
        public override void Initialize()
        {
            Name = "In South Side Chamber";
            Synonyms.Are("south", "side", "chamber");
            Description = "You are in the south side chamber.";

            NorthTo<HallOfMtKing>();
        }
    }
}
