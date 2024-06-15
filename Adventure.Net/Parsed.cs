using Adventure.Net.ActionRoutines;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Adventure.Net;

[DebuggerDisplay("{DebuggerDisplay}")]
public class Parsed
{
    private string _error;

    public TokenizedInput Input { get; set; }
    public string VerbToken { get; set; }
    public string VerbRoot { get; set; }
    public bool VerbIsAlsoPrep { get; set; }
    public List<Routine> PossibleRoutines { get; set; } = [];
    public HashSet<Object> Objects { get; set; } = [];
    public HashSet<Object> IndirectObjects { get; set; } = [];
    public List<Object> UnresolvedObjects { get; set; } = [];
    public List<Object> UnresolvedIndirectObjects { get; set; } = [];
    public string Preposition { get; set; }
    public List<WordToken> WordTokens { get; set; } = [];
    public int CurrentReadIndex { get; set; }
    public bool IsAll { get; set; }
    public bool IsExcept { get; set; }
    public bool IsHandled { get; set; }
    public bool IsPartial => PartialMessage != null;
    public string PartialMessage { get; set; }
    public string Aside { get; set; }
    public List<ParserResult> ImplicitActions { get; set; } = [];

    public bool IsError => Error != null && !IsPartial;

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
    

    // helper
    public Routine Routine => PossibleRoutines.Count == 1 ? PossibleRoutines[0] : null;

    public void Update(Parsed pr)
    {
        Input = pr.Input;
        VerbToken = pr.VerbToken;
        VerbIsAlsoPrep = pr.VerbIsAlsoPrep;
        PossibleRoutines = [.. pr.PossibleRoutines];
        Objects = [.. pr.Objects];
        IndirectObjects = [.. pr.IndirectObjects];
        UnresolvedObjects = [.. pr.UnresolvedObjects];
        UnresolvedIndirectObjects = [.. pr.UnresolvedIndirectObjects];
        Preposition = pr.Preposition;
        WordTokens = [.. pr.WordTokens];
        CurrentReadIndex = pr.CurrentReadIndex;
        Error = pr.Error;
        IsAll = pr.IsAll;
        IsExcept = pr.IsExcept;
        IsHandled = pr.IsHandled;
        PartialMessage = pr.PartialMessage;
    }

    [ExcludeFromCodeCoverage]
    private string DebuggerDisplay
    {
        get
        {
            if (Input != null)
            {
                var words = Input.Where(x => x != VerbToken).ToList();
                var list = new List<string>();

                for (int i = 0; i < words.Count; i++)
                {
                    list.Add(i == CurrentReadIndex ? $"[{words[i]}]" : $"{words[i]}");
                }

                list.Insert(0, VerbToken);

                return string.Join(' ', list);
            }

            return "No Tokens Assigned";
        }
    }

    public Parsed()
    {

    }
}
