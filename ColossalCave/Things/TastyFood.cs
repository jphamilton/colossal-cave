using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Places;

namespace ColossalCave.Things
{
    public class TastyFood : Item
    {
        public override void Initialize()
        {
            Name = "tasty food";
            Synonyms.Are("food", "ration", "rations", "tripe", "yummy", "tasty", "delicious", "scrumptious");
            Article = "some";
            Edible = true;
            Description = "Sure looks yummy!";
            InitialDescription = "There is tasty food here.";

            FoundIn<InsideBuilding>();

            After<Eat>(() =>
            {
                Print("Delicious!");
            });
            
        }
    }
}

