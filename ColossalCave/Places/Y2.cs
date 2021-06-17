using Adventure.Net;
using Adventure.Net.Actions;
using Adventure.Net.Utilities;
using ColossalCave.Actions;

namespace ColossalCave.Places
{
    public class Y2 : BelowGround
    {
        public override void Initialize()
        {
            Name = "y2";
            Description = 
                "You are in a large room, with a passage to the south, " +
                "a passage to the west, and a wall of broken rock to the east. " +
                "There is a large ~Y2~ on a rock in the room's center.";


            SouthTo<LowNSPassage>();
            EastTo<JumbleOfRock>();
            //WestTo<WindowOnPit1>();


            After<Look>(() =>
            {
                if (Random.Number(1, 100) < 25)
                {
                    Print("\r\nA hollow voice says, \"Plugh.\"\r\n");
                }
            });

            Before<Plugh>(() =>
            {
                MovePlayer.To<InsideBuilding>();
                return true;
            });

            Before<Plover>(() =>
            {
            //if (In_Plover_Room hasnt visited) rfalse;
            //if (egg_sized_emerald in player) {
            //    move egg_sized_emerald to In_Plover_Room;
            //    score = score - 5;
            //}
            //PlayerTo(In_Plover_Room);
                return true;
            });

        }
    }
}
