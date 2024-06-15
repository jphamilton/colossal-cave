using System.Collections.Generic;

namespace Adventure.Net;

public class CommandResult
{
    public List<string> Output { get; set; } = [];
    public bool Success { get; set; }
    public string Error { get; set; }
}
