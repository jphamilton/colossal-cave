using Adventure.Net;

namespace Advent.Tests
{
    public class BlackCape : Item
    {
        public override void Initialize()
        {
            Name = "black cape";
            Synonyms.Are("black", "cape");
        }
    }
}
