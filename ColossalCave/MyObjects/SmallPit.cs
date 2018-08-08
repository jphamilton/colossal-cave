namespace ColossalCave.MyObjects
{
    public class SmallPit : Scenic
    {
        public override void Initialize()
        {
            Name = "small pit";
            Synonyms.Are("pit", "small");
            Description = "The pit is breathing traces of white mist.";
        }
    }

}
