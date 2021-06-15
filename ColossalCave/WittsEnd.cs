using Adventure.Net;
using Adventure.Net.Actions;
using Adventure.Net.Utilities;

namespace ColossalCave.Places
{
    public class WittsEnd : BelowGround
    {
        public override void Initialize()
        {
            Name = "At Witt's End";
            Synonyms.Are("witt's", "witts", "end");
            Description = "You are at Witt's End. Passages lead off in *all* directions.";

            WestTo(() =>
            {
                Print("You have crawled around in some little holes and found your way blocked by a recent cave -in. You are now back in the main passage.");
                return this;
            });

            Before<Go>((Direction direction) =>
            {
                if (direction is North && Random.Number(1, 100) <= 95)
                {
                    Print("You have crawled around in some little holes and wound up back in the main passage.");
                    return true;
                }

                MovePlayer.To<Anteroom>();
                return true;
            });
        }
    }
}
