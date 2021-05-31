using Adventure.Net;

namespace Tests
{
    public class RedHat : Item
    {
        public override void Initialize()
        {
            Name = "red hat";
            Synonyms.Are("red", "hat");
            Article = "the";
        }
    }

    public class BlackHat : Item
    {
        public override void Initialize()
        {
            Name = "black hat";
            Synonyms.Are("black", "hat");
            Article = "the";
        }
    }

    public class WhiteHat : Item
    {
        public override void Initialize()
        {
            Name = "white hat";
            Synonyms.Are("white", "hat");
            Article = "the";
        }
    }

}
