using Adventure.Net.ActionRoutines;
using System.Collections.Generic;
using System.Linq;

namespace Adventure.Net;

public class ParserResult
{
    private string _error;

    public Routine Routine { get; set; }
    public List<Object> Objects { get; set; } = [];
    public Object IndirectObject { get; set; }
    public Parsed Parsed { get; }
    public bool IsHandled { get; set; }
    public bool IsError => Error != null && !IsPartial;
    public bool IsPartial => PartialMessage != null;
    public string PartialMessage { get; set; }
    public string Aside { get; set; }

    public string Error
    {
        get
        {
            return PartialMessage ?? _error;
        }
        set
        {
            _error = !string.IsNullOrEmpty(value) ? value : null;
        }
    }

    public ParserResult()
    {

    }

    public ParserResult(Parsed p)
    {
        Aside = p.Aside;
        Error = p.Error;
        IndirectObject = p.IndirectObjects.FirstOrDefault();
        IsHandled = p.IsHandled;
        Objects = [.. p.Objects];
        Parsed = p;
        PartialMessage = p.PartialMessage;
        Routine = p.PossibleRoutines.Count > 0 ? p.PossibleRoutines[0] : null;
    }
}
