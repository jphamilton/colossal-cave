using Adventure.Net.Actions;
using ColossalCave.Places;

namespace ColossalCave.Things
{
    public class PitCrack : Scenic
    {
        public override void Initialize()
        {
            Name = "crack";
            Synonyms.Are("crack", "small");
            Description = "The crack is very small -- far too small for you to follow.";

            FoundIn<TopOfSmallPit>();

            Before<Enter>(() =>
            {
                Print("The crack is far too small for you to follow.");
                return true;
            });
        }
    }

}
