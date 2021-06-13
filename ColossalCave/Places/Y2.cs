using Adventure.Net;
using Adventure.Net.Verbs;
using ColossalCave.Objects;
using ColossalCave.Verbs;

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

            Has<Y2Rock>();

            After<Look>(() =>
            {

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

/*
 Room    At_Y2 "At ~Y2~"
  with  name 'y2',
        description
            "You are in a large room, with a passage to the south,
             a passage to the west, and a wall of broken rock to the east.
             There is a large ~Y2~ on a rock in the room's center.",
        after [;
          Look:
            if (random(100) <= 25) print "^A hollow voice says, ~Plugh.~^";
        ],
        before [;
          Plugh:
            PlayerTo(Inside_Building);
            rtrue;
          Plover:
            if (In_Plover_Room hasnt visited) rfalse;
            if (egg_sized_emerald in player) {
                move egg_sized_emerald to In_Plover_Room;
                score = score - 5;
            }
            PlayerTo(In_Plover_Room);
            rtrue;
        ],
        s_to Low_N_S_Passage,
        e_to Jumble_Of_Rock,
        w_to At_Window_On_Pit_1;

Scenic  -> "~Y2~ rock"
  with  name 'rock' 'y2',
        description "There is a large ~Y2~ painted on the rock.",
  has   supporter;
 
 */
