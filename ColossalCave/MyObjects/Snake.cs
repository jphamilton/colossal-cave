using Adventure.Net;
using Adventure.Net.Verbs;

namespace ColossalCave.MyObjects
{
    public class Snake : Object
    {
        public override void Initialize()
        {
            Name = "snake";
            Synonyms.Are("cobra", "asp", "huge", "fierce", "green", "ferocious", 
                "venemous", "venomous", "large", "big", "killer" );
            Description = "I wouldn't mess with it if I were you.";
            InitialDescription = "A huge green fierce snake bars the way!";
            IsAnimate = true;

        }
    }
}