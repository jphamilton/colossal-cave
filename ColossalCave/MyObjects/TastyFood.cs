using Adventure.Net;
using Adventure.Net.Verbs;

namespace ColossalCave.MyObjects
{
    public class TastyFood : Object
    {
        public override void Initialize()
        {
            Name = "tasty food";
            Synonyms.Are("food", "ration", "rations", "tripe", "yummy", "tasty", "delicious", "scrumptious");
            Article = "some";
            IsEdible = true;
            Description = "Sure looks yummy!";
            InitialDescription = "There is tasty food here.";

            After<Eat>(() =>
                {
                    Print("Delicious!");
                    return true;
                });
            
        }
    }
}

