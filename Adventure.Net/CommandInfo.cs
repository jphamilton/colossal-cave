using System;
using System.Collections.Generic;

namespace Adventure.Net
{
    public class CommandInfo
    {
        public Verb Verb { get; set; }
        public List<string> Objects { get; private set; }
        public string IndirectObject { get; set; }
        public bool ObjectMustBeHeld { get; set; }
        public bool IndirectObjectMustBeHeld { get; set; }
        public string Direction { get; set; }
        public Func<string> Action { get; set; }
        public bool IsAll { get; set; }

        public CommandInfo()
        {
            Objects = new List<string>();
        }
    }
}
