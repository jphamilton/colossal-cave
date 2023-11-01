using System.Linq;

namespace Adventure.Net.Actions;

public class Goto : Verb
{
    public Goto()
    {
        Name = "goto";
    }

    public bool Expects()
    {
        Output.Print("Where do you want to go?");

        var x = CommandPrompt.GetInput();

        var room = Objects.All.Where(r => r is Room && r.GetType().Name.Contains(x)).FirstOrDefault();

        if (room != null)
        {
            MovePlayer.To((Room)room);
        }

        return true;
    }
}
