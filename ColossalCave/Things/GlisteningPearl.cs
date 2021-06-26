
using Adventure.Net;

namespace ColossalCave.Things
{
    public class GlisteningPearl : Treasure
    {
        public override void Initialize()
        {
            Name = "glistening pearl";
            Synonyms.Are("pearl", "glistening", "incredible", "incredibly", "large");
            Description = "It's incredibly large!";
            InitialDescription = "Off to one side lies a glistening pearl!";
            DepositPoints = 14;
        }
    }
}
