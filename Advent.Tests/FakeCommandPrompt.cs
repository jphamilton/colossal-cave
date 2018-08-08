using System.IO;
using Adventure.Net;

namespace Advent.Tests
{
    public class FakeCommandPrompt : CommandPrompt
    {
        public FakeCommandPrompt(string input) : base(new StringWriter(), new StringReader(input))
        {

        }

        public StringWriter FakeOutput
        {
            get { return (StringWriter)output; }
        }
    }
}
