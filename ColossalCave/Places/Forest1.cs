using System;
using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Things;

namespace ColossalCave.Places
{
    public class Forest1 : AboveGround
    {
        public override void Initialize()
        {
            Name = "In Forest";
            Synonyms.Are("forest");
            Description = "You are in open forest, with a deep valley to one side.";

            EastTo<Valley>();
            DownTo<Valley>();
            NorthTo<Forest1>();
            WestTo<Forest1>();
            SouthTo<Forest1>();

            Has<Forest>();

            Before<Enter>(() => {

                var rnd = new Random(DateTime.Now.Second);
                
                if (rnd.Next(1, 2) == 1)
                {
                    MovePlayer.To(Room<Forest2>());
                    return true;
                }

                return false;
            });

        }
    }
}


