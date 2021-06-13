namespace ColossalCave.Things
{
    public class TreasureChest : Treasure
    {
        public override void Initialize()
        {
            Name = "treasure chest";
            Synonyms.Are("chest", "box", "treasure", "riches", "pirate", "pirate's", "treasure", "booty");
            Description = "It's the pirate's treasure chest, filled with riches of all kinds!";
            InitialDescription = "The pirate's treasure chest is here!";
            DepositPoints = 12;
        }
    }
}
