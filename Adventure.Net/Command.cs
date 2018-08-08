using System;

namespace Adventure.Net
{
    public class Command
    {
        public Verb Verb { get; set; }
        public Object Object { get; set; }
        public Object IndirectObject { get; set; }
        public Func<bool> Action { get; set; }
    }
}
