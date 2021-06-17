using Adventure.Net;
using ColossalCave.Things;

namespace ColossalCave.Places
{
    public class WestSideChamber : BelowGround
    {
        public override void Initialize()
        {
            Name = "In West Side Chamber";
            Synonyms.Are("wide", "chamber");
            Description = 
                "You are in the west side chamber of the hall of the mountain king. " +
                "A passage continues west and up here.";

            WestTo<CrossOver>();
            UpTo<CrossOver>();
            EastTo<HallOfMtKing>();
        }
    }
}
