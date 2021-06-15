using Adventure.Net;
using Adventure.Net.Actions;
using Adventure.Net.Utilities;
using ColossalCave.Things;

namespace ColossalCave.Places
{
    public class AtopStalactite : BelowGround
    {
        public override void Initialize()
        {
            Name = "Atop Stalactite";
            Synonyms.Are("atop", "stalactite");
            Description = 
                "A large stalactite extends from the roof and almost reaches the floor below. " +
                "You could climb down it, and jump from it to the floor, " +
                "but having done so you would be unable to reach it to climb back up.";

            Has<Stalactite>();

            NorthTo<SecretNSCanyon1>();

            DownTo(() =>
            {
                if (Random.Number(1, 100) <= 40)
                    return Rooms.Get<AlikeMaze6>();
                if (Random.Number(1, 100) <= 50)
                    return Rooms.Get<AlikeMaze9>();
                return Rooms.Get<AlikeMaze4>();
            });

            Before<Climb>(() =>
            {
                MovePlayer.To(DOWN());
                return true;
            });
        }
    }
}
