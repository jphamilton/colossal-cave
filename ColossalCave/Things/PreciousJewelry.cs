namespace ColossalCave.Things
{
    public class PreciousJewelry : Treasure
    {
        public override void Initialize()
        {
            Name = "precious jewelry";
            Synonyms.Are("jewel", "jewels", "jewelry", "precious", "exquisite");
            Description = "It's all quite exquisite!";
            InitialDescription = "There is precious jewelry here!";
        }
    }
}
