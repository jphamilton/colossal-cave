namespace ColossalCave.Objects
{
    public class CrudeNote : Scenic
    {
        public override void Initialize()
        {
            Name = "note";
            Synonyms.Are("note", "crude");
            Description = "The note says, \"You won't get it up the steps\".";
        }
    }
}
