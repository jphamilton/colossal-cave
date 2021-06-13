using Adventure.Net.Extensions;

namespace Adventure.Net.Utilities
{
    public static class YesOrNo
    {
        public static bool Ask(string question = null)
        {
            if (question != null)
            {
                Output.Print(question);
            }

            while (true)
            {
                string[] affirmative = new[] { "y", "yes", "yep", "yeah" };
                string[] negative = new[] { "n", "no", "nope", "nah", "naw", "nada" };
                string response = CommandPrompt.GetInput();
                if (!response.In(affirmative) && !response.In(negative))
                {
                    Output.Print("Please answer yes or no.");
                }
                else
                {
                    return (response.In(affirmative));
                }
            }
        }
    }
}
