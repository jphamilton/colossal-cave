namespace ColossalCave.MyRooms
{
    public class AwkwardSlopingEWCanyon : AdventRoom
    {
        public override void Initialize()
        {
            Name = "Sloping E/W Canyon";
            Synonyms.Are("sloping", "e/w", "canyon");
            Description = "You are in an awkward sloping east/west canyon.";
            DownTo<DebrisRoom>();
            EastTo<DebrisRoom>();
            UpTo<BirdChamber>();
            WestTo<BirdChamber>();
            NoDwarf = true;
        }
    }
}

