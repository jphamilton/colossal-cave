using Adventure.Net;

namespace ColossalCave.MyObjects
{
    public class Dragon : Object
    {
        public override void Initialize()
        {
            Name = "dragon";
            Synonyms.Are("monster", "beast", "lizard", "huge", "green", "fierce", "scaly", "giant", "ferocious");
            Description = "I wouldn't mess with it if I were you.";
            InitialDescription = "A huge green fierce dragon bars the way!";
            IsAnimate = true;
        }
    }
}