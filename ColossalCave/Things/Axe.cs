using Adventure.Net;
using Adventure.Net.Actions;

namespace ColossalCave.Things
{
    public class Axe : Object
    {
        public bool IsNearBear { get; set; } = false;

        public override void Initialize()
        {
            Name = "dwarvish axe";
            Synonyms.Are("axe", "little", "dwarvish", "dwarven");
            InitialDescription = "There is a little axe here.";

            Before<Examine>(() =>
            {
                if (IsNearBear)
                {
                    return false;
                }

                Print("It's lying beside the bear.");
                
                return true;
            });

            Before<Take>(() =>
            {
                if (!IsNearBear)
                {
                    return false;
                }

                Print("No chance. It's lying beside the ferocious bear, quite within harm's way.");
                return true;
            });
        }
    }
}
