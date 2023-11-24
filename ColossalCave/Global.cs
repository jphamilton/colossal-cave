using Adventure.Net;

namespace ColossalCave;

public static class Global
{
    public const int MaxTreasures = 15;

    private static State _state;

    public static State State
    {
        get
        {
            if (_state == null)
            {
                _state = Objects.Get<State>();
            }

            return _state;
        }
    }

    //public static bool CavesClosed { get; set; }
    //public static Room CanyonFrom { get; set; }
    //public static int TreasuresFound { get; set; }
    //public static int Deaths { get; set; } = 0;
}

public class State : Object
{
    public override void Initialize() {}

    public bool CavesClosed { get; set; }
    public Room CanyonFrom { get; set; }
    public int TreasuresFound { get; set; }
    public int Deaths { get; set; }
}
