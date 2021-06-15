using Adventure.Net;
using Adventure.Net.Actions;

namespace ColossalCave.Things
{
    public class Sign : Item
    {
        public override void Initialize()
        {
            Name = "sign";
            Synonyms.Are("sign", "witt", "company", "construction");
            IsStatic = true;
            InitialDescription = 
                "A sign in midair here says \"Cave under construction beyond this point. " +
                "Proceed at own risk. [Witt Construction Company]\"";

            Before<Take>(() =>
            {
                Print("It's hanging way above your head.");
                return true;
            });
        }
    }
}
