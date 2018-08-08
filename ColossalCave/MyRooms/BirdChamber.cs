using ColossalCave.MyObjects;

namespace ColossalCave.MyRooms
{
    public class BirdChamber : AdventRoom
    {
        public override void Initialize()
        {
            Name = "Orange River Chamber";
            Synonyms.Are("Orange", "River", "Chamber");
            Description = "You are in a splendid chamber thirty feet high. " +
                "The walls are frozen rivers of orange stone. " +
                "An awkward canyon and a good passage exit from east and west sides of the chamber.";
            EastTo<AwkwardSlopingEWCanyon>();
            WestTo<TopOfSmallPit>();
            NoDwarf = true;

            Has<LittleBird>();
        }
    }

//Room    In_Bird_Chamber "Orange River Chamber"
//with  name 'orange' 'river' 'chamber',
//    description
//        "You are in a splendid chamber thirty feet high.
//         The walls are frozen rivers of orange stone.
//         An awkward canyon and a good passage exit from east and west sides of the chamber.",
//    e_to In_Awkward_Sloping_E_W_Canyon,
//    w_to At_Top_Of_Small_Pit,
//has   nodwarf;

}
