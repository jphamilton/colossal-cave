using Adventure.Net;

namespace ColossalCave.Places
{
    public class SlopingCorridor : BelowGround
    {
        public override void Initialize()
        {
            Name = "Sloping Corridor";
            Synonyms.Are("sloping", "corridor");
            Description = "You are in a long winding corridor sloping out of sight in both directions.";

            CantGo = "The corridor slopes steeply up and down.";

            DownTo<LargeLowRoom>();
            UpTo<SwSideOfChasm>();

        }
    }
}
