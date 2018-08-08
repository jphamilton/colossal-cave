using System.Text;

namespace Adventure.Net
{
    public static class StringBuilderExtensions
    {
        public static void Indent(this StringBuilder input, int level)
        {
            input.Append(new string('\t', level));
        }
    }
}
