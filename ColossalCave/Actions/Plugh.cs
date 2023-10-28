using Adventure.Net;

namespace ColossalCave.Actions;

public class Plugh : Verb
{
    public Plugh()
    {
        Name = "plugh";
    }

    public bool Expects()
    {
        Print("Nothing happens.");
        return true;
    }
}
