namespace ColossalCave.Places
{
    public class CanyonDeadEnd : BelowGround
    {
        public override void Initialize()
        {
            Name = "Canyon Dead End";
            Description = "The canyon here becomes too tight to go further south.";

            NorthTo<NSCanyon>();

        }
    }
}
