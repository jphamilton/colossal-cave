using System.Collections.Generic;
using System.Globalization;

namespace Adventure.Net.Extensions;

public static class StringExtensions
{
    public static bool HasValue(this string input)
    {
        return !string.IsNullOrEmpty(input) && !string.IsNullOrWhiteSpace(input);
    }

    public static bool In(this string input, IList<string> values)
    {
        return values.Contains(input);
    }

    public static string Capitalize(this string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        if (input.Length == 1)
        {
            return char.ToUpper(input[0], CultureInfo.CurrentCulture).ToString();
        }

        return char.ToUpper(input[0], CultureInfo.CurrentCulture) + input[1..];
    }

}
