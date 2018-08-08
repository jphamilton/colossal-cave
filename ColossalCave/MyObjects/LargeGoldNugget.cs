namespace ColossalCave.MyObjects
{
    public class LargeGoldNugget : Treasure
    {
        public override void Initialize()
        {
            Name = "large gold nugget";
            Synonyms.Are("gold", "nugget", "large", "heavy");
            Description = "It's a large sparkling nugget of gold!";
            InitialDescription = "There is a large sparkling nugget of gold here!";
        }
    }

}
