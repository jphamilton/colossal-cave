using System.Diagnostics;

namespace Adventure.Net;

[DebuggerDisplay("{Name} - {Type}")]
public class WordToken
{
    public string Name { get; set; }
    public WordFlags Type { get; set; }
    public bool IsObject => (Type & WordFlags.Object) != 0;
    public bool IsDoor => (Type & WordFlags.Door) != 0;
    public bool IsRoom => (Type & WordFlags.Room) != 0;
    public bool IsVerb => (Type & WordFlags.Verb) != 0;
    public bool IsDirection => (Type & WordFlags.Direction) != 0;
    public bool IsPreposition => (Type & WordFlags.Preposition) != 0;
    public bool IsAll => Name == "all";
    public bool IsExcept => Name == "except";

    public WordToken(string token, WordFlags type)
    {
        Name = token;
        Type = type;
    }
}
