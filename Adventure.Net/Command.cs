using System;

namespace Adventure.Net
{
    public class Command
    {
        public Verb Verb { get; set; }
        public Item Object { get; set; }
        public Item IndirectObject { get; set; }
        public Func<bool> Action { get; set; }
    }
}
