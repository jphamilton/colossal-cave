namespace ColossalCave.Places
{
    public class NSCanyon : BelowGround
    {
        public override void Initialize()
        {
            Name = "N/S Canyon";
            Synonyms.Are("n/s", "canyon");
            Description = "You are at a wide place in a very tight N/S canyon.";
            
            SouthTo<CanyonDeadEnd>();
            
            NorthTo<TallEWCanyon>();
        }
    }
}
