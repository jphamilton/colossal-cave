using ColossalCave.Things;

namespace ColossalCave.Places
{
    public class BirdChamber : BelowGround
    {
        public override void Initialize()
        {
            Name = "Orange River Chamber";
            Synonyms.Are("orange", "river", "chamber");
            Description = "You are in a splendid chamber thirty feet high. " +
                "The walls are frozen rivers of orange stone. " +
                "An awkward canyon and a good passage exit from east and west sides of the chamber.";
            EastTo<AwkwardSlopingEWCanyon>();
            WestTo<TopOfSmallPit>();
            NoDwarf = true;

            Has<LittleBird>();
        }
    }

}
