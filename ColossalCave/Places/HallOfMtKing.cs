using ColossalCave.Things;

namespace ColossalCave.Places
{
    public class HallOfMtKing : BelowGround
    {
        public override void Initialize()
        {
            Name = "Hall of the Mountain King";
            Description = "You are in the hall of the mountain king, with passages off in all directions.";
            CantGo = "Well, perhaps not quite all directions.";

            Has<Snake>();

            UpTo<HallOfMists>();
            EastTo<HallOfMists>();
            //NorthTo<LowNSPassage>();
            //SouthTo<SouthSideChamber>();
            //WestTo<WestSideChamber>();
            //SouthWestTo<SecretEWCanyon>();
        }
     }
}

/*
 Room    In_Hall_Of_Mt_King "Hall of the Mountain King"
  with  name 'hall' 'of' 'mountain' 'king',
        description
            "You are in the hall of the mountain king, with passages off in all directions.",
        cant_go "Well, perhaps not quite all directions.",
        u_to In_Hall_Of_Mists,
        e_to In_Hall_Of_Mists,
        n_to Low_N_S_Passage,
        s_to In_South_Side_Chamber,
        w_to In_West_Side_Chamber,
        sw_to In_Secret_E_W_Canyon,
        before [;
          Go:
            if (Snake in self && (noun == n_obj or s_obj or w_obj ||
                                 (noun == sw_obj && random(100) <= 35)))
                "You can't get by the snake.";
        ];
 
 */