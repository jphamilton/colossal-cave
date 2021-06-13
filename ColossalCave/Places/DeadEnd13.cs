using ColossalCave.Objects;

namespace ColossalCave.Places
{
    public class DeadEnd13 : DeadEnd
    {
        public override void Initialize()
        {
            base.Initialize();
            
            NoDwarf = true;

            Description = "This is the pirate's dead end.";

            Has<TreasureChest>();

            SouthEastTo<AlikeMaze13>();
            
            OutTo<AlikeMaze13>();

            Initial = () =>
            {
                //StopDaemon(Pirate);

                //if (treasure_chest in self && treasure_chest hasnt moved)
                //                "You've found the pirate's treasure chest!";
                //        ],
            };
        }
    }
}

