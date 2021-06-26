namespace ColossalCave.Places
{
    public class WestEndOfLongHall : BelowGround
    {
        public override void Initialize()
        {
            Name = "At West End of Long Hall";
            Synonyms.Are("long", "hall");
            Description = "You are at the west end of a very long featureless hall. The hall joins up with a narrow north/south passage.";

            EastTo<EastEndOfLongHall>();
            SouthTo<DifferentMaze1>();
            NorthTo<CrossOver>();
        }
    }
}
