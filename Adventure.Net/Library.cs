using Adventure.Net.Extensions;
using System.Collections.Generic;

namespace Adventure.Net
{
    public static class Library
    {
        static Library()
        {
            CantSeeObject = "You can't see any such thing.";
            DoNotUnderstand = "I beg your pardon?";
            DidntUnderstandSentence = "I didn't understand that sentence.";
            VerbNotRecognized = "That's not a verb I recognize.";
        }

        private static IStory Story
        {
            get { return Context.Story; }
        }

        public static string DoNotUnderstand { get; set;}
        public static string DidntUnderstandSentence { get; set;}
        public static string CantSeeObject { get; set; }
        public static string VerbNotRecognized { get; set; }

        public static string PartialUnderstanding(Verb verb, INamed named)
        {
            return $"I only understood you as far as wanting to {verb.Name} the {named.Name}.";
        }

        public static void Quit()
        {
            if (YesOrNo("Are you sure you want to quit?"))
            {
                Story.IsDone = true;
            }
        }

        public static bool YesOrNo(string question)
        {
            Output.Print(question);

            while (true)
            {
                string[] affirmative = new[]{"y", "yes", "yep", "yeah"};
                string[] negative = new[]{"n", "no", "nope", "nah", "naw"};
                string response = CommandPrompt.GetInput();
                if (!response.In(affirmative) && !response.In(negative))
                    Output.Print("Please answer yes or no.");
                else
                    return (response.In(affirmative));
            }
        }

        public static void MovePlayerTo<T>() where T:Room
        {
            var room = Rooms.Get<T>();
            MovePlayerTo(room);
        }

        public static void MovePlayerTo(Room room)
        {
            Room real = room;

            if (!CurrentRoom.IsLit())
            {
                room = Rooms.Get<Darkness>();
            }

            Story.Location = room;

            if (!room.Visited && room.Initial != null)
            {
                room.Initial();
            }
            else
            {
                if (!CurrentRoom.IsLit() && room.Visited)
                {
                    real.DarkToDark();
                }

                CurrentRoom.Look(false);
            }
                
            
            room.Visited = true;

            Story.Location = real;
        }

        public static void RunDaemons()
        {
            IList<Item> objectsWithDaemons = Items.WithRunningDaemons();
            foreach (var obj in objectsWithDaemons)
                obj.Daemon();
        }

    }
}
