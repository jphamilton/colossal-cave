using Adventure.Net.Extensions;

namespace Adventure.Net.Utilities;

public static class YesOrNo
{
    public static bool Ask(string question = null)
    {
        if (question != null)
        {
            Output.Print(question, true);
        }

        while (true)
        {
            string[] affirmative = ["y", "yes", "yep", "yeah"];
            string[] negative = ["n", "no", "nope", "nah", "naw"];
            string response = CommandPrompt.GetInput();
            
            if (!response.In(affirmative) && !response.In(negative))
            {
                Output.Print("Please answer yes or no.", true);
            }
            else
            {
                return (response.In(affirmative));
            }
        }
    }
}
