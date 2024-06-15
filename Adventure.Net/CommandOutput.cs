using System.Collections.Generic;

namespace Adventure.Net;

public class CommandOutput : ICommandState
{
    public CommandState State { get; set; }

    public IList<string> BeforeOutput { get; } = [];
    public IList<string> DuringOutput { get; } = [];
    public IList<string> AfterOutput { get; } = [];

    public IList<string> Messages
    {
        get
        {
            var result = new List<string>();

            result.AddRange(BeforeOutput);

            // After messages are shown before During messages which,
            // allows objects to add "asides" to mundane actions.
            //
            // An example is the dropping the Ming Vase in Colossal Cave:
            //
            // > drop vase
            // (coming to rest, delicately, on the velvet pillow)
            // Dropped.
            result.AddRange(AfterOutput);

            result.AddRange(DuringOutput);

            return result;
        }
    }
}
