using Adventure.Net;
using Adventure.Net.Actions;

namespace ColossalCave.Things
{
    public class TastyFood : Item
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
            });
            
        }
    }
}

