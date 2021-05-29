using System.Collections.Generic;

namespace Adventure.Net
{
    public class CommandLineParserResult
    {
        public Verb Verb { get; set; }

        public List<Item> Objects { get; set;  } = new List<Item>();

        public Prep Preposition { get; set; }

        public Item IndirectObject { get; set; }

        public string Error { get; set; }

        // "all" has been specified on command line
        public bool IsAll { get; set; }

        public List<object> Ordered { get; } = new List<object>();

        // TODO: something better?
        // These are messages that need to be displayed before the command is run
        public IList<string> PreOutput { get; } = new List<string>();

        public CommandHandler CommandHandler()
        {
            return new CommandHandler(this);
        }
    }
}
