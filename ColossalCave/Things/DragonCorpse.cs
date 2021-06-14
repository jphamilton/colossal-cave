using Adventure.Net;
using Adventure.Net.Actions;

namespace ColossalCave.Things
{
    public class DragonCorpse : Item
    {
        public override void Initialize()
        {
            Name = "dragon's body";
            Synonyms.Are("dragon", "corpse", "dead", "dragon's", "body");
            InitialDescription = "The body of a huge green dead dragon is lying off to one side.";
            IsStatic = true;

            Before<Attack>(() =>
            {
                Print("You've already done enough damage!");
                return true;
            });
        }
    }
}
