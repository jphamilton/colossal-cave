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

        public CommandHandler CommandHandler()
        {
            return new CommandHandler(this);
        }
    }
}
