using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Adventure.Net;

[DebuggerDisplay("{Object.Name}")]
public class SaveObject
{
    public int Id { get; set; }
    public int T { get; set; }
    public int P { get; set; }
    public List<int> C { get; set; } = [];
    public string A { get; set; }
    public int AX { get; set; }
    public List<double> N { get; set; } = [];
    public List<string> S { get; set; } = [];
    public List<int> O { get; set; } = [];

    public Object Object { get; }

    [JsonConstructor]
    public SaveObject()
    {
        // for serialization
    }

    public SaveObject(Object obj)
    {
        Id = obj.Id;
        Object = obj;
    }
}
