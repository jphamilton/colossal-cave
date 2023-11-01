using System.Collections.Generic;

namespace Adventure.Net;

public class CommandResult
{
    public List<string> Output { get; } = new List<string>();
    public bool Success { get; set; }
    public string Error { get; set; }
}
