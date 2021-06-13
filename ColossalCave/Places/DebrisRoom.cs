using Adventure.Net;
using ColossalCave.Things;
using ColossalCave.Actions;

namespace ColossalCave.Places
{
    public class DebrisRoom : BelowGround
    {
        public override void Initialize()
        {
            Name = "In Debris Room";
            Synonyms.Are("debris", "room");
            Description =
                "You are in a debris room filled with stuff washed in from the surface. " +
                "A low wide passage with cobbles becomes plugged with mud and debris here, " +
                "but an awkward canyon leads upward and west.\n\n" +
                "A note on the wall says, \"Magic word XYZZY.\"";

            EastTo<CobbleCrawl>();
            UpTo<AwkwardSlopingEWCanyon>();
            WestTo<AwkwardSlopingEWCanyon>();

            Before<Xyzzy>(() =>
                {
                    MovePlayer.To<InsideBuilding>();
                    return true;
                });

            NoDwarf = true;

            Has<Debris>();
            Has<Cobbles>();
            Has<Note>();
        }
    }
}


