namespace ColossalCave.Things
{
    public class RareCoins : Treasure
    {
        public override void Initialize()
        {
            Name = "rare coins";
            Synonyms.Are("coins", "rare");
            Article = "some";
            Description = "They're a numismatist's dream!";
            InitialDescription = "There are many coins here!";
            // has multitude
        }
    }
}
