using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Places;

namespace ColossalCave.Things;

public class PersianRug : Treasure
{
    public override void Initialize()
    {
        Name = "Persian rug";
        Synonyms.Are("rug", "persian", "persian", "fine", "finest", "dragon's");

        FoundIn<SecretCanyon>();

        Describe = () =>
        {
            if (DragonIsHere)
            {
                return "The dragon is sprawled out on the Persian rug!";
            }

            return "The Persian rug is spread out on the floor here.";
        };

        Before<Take>(() =>
        {
            if (DragonIsHere)
            {
                Print("You'll need to get the dragon to move first!");
                return true;
            }

            return false;
        });
    }

    private bool DragonIsHere
    {
        get
        {
            var dragon = Objects.Get<Dragon>();
            return dragon.InRoom;
        }
    }
}
