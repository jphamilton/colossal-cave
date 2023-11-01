
namespace Adventure.Net.Actions;

public class Wave : Verb
{
    public Wave()
    {
        Name = "wave";
    }

    public bool Expects()
    {
        return Redirect<WaveHands>(v => v.Expects());
    }

    public bool Expects(Preposition.At at, Object obj)
    {
        return Redirect<WaveHands>(obj, v => v.Expects(at, obj));
    }

    public bool Expects(Object obj)
    {
        Print($"You look ridiculous waving {obj.DefiniteArticle} {obj.Name}.");
        return true;
    }

    public bool Expects(Object obj, Preposition.At at, Object indirect)
    {
        Print($"You wave {obj.DefiniteArticle} {obj.Name} at {obj.DefiniteArticle} {indirect}, feeling foolish.");
        return true;
    }
}