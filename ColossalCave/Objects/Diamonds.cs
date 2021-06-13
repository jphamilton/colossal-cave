namespace ColossalCave.Objects
{
    public class Diamonds : Treasure
    {
        public override void Initialize()
        {
            Name = "diamonds";
            Synonyms.Are("diamond", "diamonds", "several", "high", "quality");
            Description = "They look to be of the highest quality!";
            InitialDescription = "There are diamonds here!";
            Article = "some";
            // has multitude
        }
    }
}
