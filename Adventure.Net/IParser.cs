using System.Collections.Generic;

namespace Adventure.Net
{
    public interface IParser : IPrintable
    {
        IList<string> Parse(string text);
        bool ExecuteCommand(Command command);
    }
}