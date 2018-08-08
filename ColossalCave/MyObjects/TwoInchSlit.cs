using Adventure.Net.Verbs;

namespace ColossalCave.MyObjects
{
    public class TwoInchSlit : Scenic
    {
        public override void Initialize()
        {
            Name = "2-inch slit"; 
            Synonyms.Are("slit", "two", "inch", "2-inch");
            Description = "It's just a 2-inch slit in the rock, through which the stream is flowing.";

            Before<Enter>(()=>
                {
                    Print("You don't fit through a two-inch slit!");
                    return true;
                });

        }
    }
}

