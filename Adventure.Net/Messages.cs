
namespace Adventure.Net
{
    public static class Messages
    {
        static Messages()
        {
            CantSeeObject = "You can't see any such thing.";
            DoNotUnderstand = "I beg your pardon?";
            DidntUnderstandSentence = "I didn't understand that sentence.";
            VerbNotRecognized = "That's not a verb I recognize.";
            Partial = "I only understood you as far as wanting to {0} the {1}."; // verb article object
            MultiNotAllowed = "You can't use multiple objects with that verb.";
        }

        public static string CantSeeObject { get; set; }
        public static string DidntUnderstandSentence { get; set; }
        public static string DoNotUnderstand { get; set; }
        public static string MultiNotAllowed { get; set; }
        public static string Partial { get; set; }
        public static string VerbNotRecognized { get; set; }

        public static string PartialUnderstanding(Verb verb, Item obj)
        {
            return string.Format(Partial, verb.Name, obj.Name);
        }
    }
}
