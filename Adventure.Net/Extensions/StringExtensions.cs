using System.Collections.Generic;
using System.Globalization;

namespace Adventure.Net.Extensions;

public static class StringExtensions
{

    public static string TitleCase(this string input)
    {
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        return textInfo.ToTitleCase(input);
    }

    public static bool HasValue(this string input)
    {
        return !string.IsNullOrEmpty(input) && !string.IsNullOrWhiteSpace(input);
    }

    public static bool In(this string input, IList<string> values)
    {
        return (values.Contains(input));
    }

    public static bool IsPreposition(this string input)
    {
        return Prepositions.Contains(input);
    }

    public static bool IsDirection(this string input)
    {
        return Compass.Directions.Contains(input);
    }

    public static Verb ToVerb(this string input)
    {
        return Verbs.Get(input);
    }

    public static string Capitalize(this string input)
    {
        if (!string.IsNullOrEmpty(input))
        {
            if (input.Length == 1)
            {
                input = input.ToUpper();
            }
            else
            {
                input = char.ToUpper(input[0]) + input[1..];
            }
        }

        return input;
    }

}
