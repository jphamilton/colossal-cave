using Adventure.Net;

namespace ColossalCave.Actions;

public class Water : Verb
{
    public Water()
    {
        Name = "water";
    }

    public bool Expects(Object obj)
    {
        return true;
    }
}
