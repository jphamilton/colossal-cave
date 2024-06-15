using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Adventure.Net.Extensions;

[ExcludeFromCodeCoverage]
public static class StringBuilderExtensions
{
    public static void Indent(this StringBuilder input, int level)
    {
        input.Append(new string('\t', level));
    }
}
