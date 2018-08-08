namespace ColossalCave.MyObjects
{
    public class SewerPipes : Scenic
    {
        public override void Initialize()
        {
            Name = "pair of 1 foot diameter sewer pipes"; 
            Synonyms.Are("pipes", "pipe", "one", "foot", "diameter", "sewer", "sewer-pipes");
            Description = "Too small. The only exit is to the west.";
        }
    }
}

