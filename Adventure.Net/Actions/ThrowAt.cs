namespace Adventure.Net.Actions;

public class ThrowAt : Verb
{
    public ThrowAt()
    {
        Name = "throw at";
    }

    public bool Expects([Held] Object obj, Preposition.At at, Object indirect)
    {
        if (indirect.Animate)
        {
            Print("You lack the nerve when it comes to the crucial moment.");
        }
        else
        {
            Print("Futile.");
        }

        return true;
    }

}
