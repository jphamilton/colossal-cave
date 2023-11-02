

using Adventure.Net;
using Adventure.Net.Actions;

namespace Tests.ObjectTests
{
    public class BagOfRocks : Object
    {
        public override void Initialize()
        {
            Name = "rocks";
            Synonyms.Are("bag");

            Before<Take>(() => Print("The bag is too heavy."));
        }
    }
}
