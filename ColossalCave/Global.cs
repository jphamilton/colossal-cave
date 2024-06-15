using Adventure.Net;

namespace ColossalCave;

public static class Global
{
    public const int MaxTreasures = 15;

    public static State State => Objects.Get<State>();
}

public class State : Object
{
    public override void Initialize()
    {

    }

    public bool CavesClosed { get; set; }
    public Room CanyonFrom { get; set; }
    public int TreasuresFound { get; set; }
    public int Deaths { get; set; }
}
