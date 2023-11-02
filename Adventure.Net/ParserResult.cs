using System.Collections.Generic;
using System.Reflection;

namespace Adventure.Net;

public class Parameters
{
    public List<Object> Objects { get; set; } = new List<Object>();

    public Prep Preposition { get; set; }

    public Object IndirectObject { get; set; }

    public List<string> Key
    {
        get
        {
            var key = new List<string>();

            if (Objects.Count > 0)
            {
                key.Add("obj");
            }

            if (Preposition != null)
            {
                key.Add($"{Preposition}");
            }

            if (IndirectObject != null)
            {
                key.Add("obj");
            }

            return key;
        }
    }

    public override string ToString()
    {
        return string.Join('.', Key);
    }
}

public class ParserResult : Parameters
{
    public Verb Verb { get; set; }
    public string VerbToken { get; set; }
    public string Error { get; set; }
    public bool IsAll { get; set; }
    public Object ImplicitTake { get; set; }
    public List<object> Ordered { get; } = new();
    public TokenizedInput Tokens { get; set; }
    public MethodInfo Expects { get; set; }

    public List<string> Input
    {
        get
        {
            var command = new List<string> { VerbToken ?? ""};
            command.AddRange(Tokens);
            return command;
        }
    }

    public CommandHandler CommandHandler()
    {
        return new CommandHandler(this);
    }
}
