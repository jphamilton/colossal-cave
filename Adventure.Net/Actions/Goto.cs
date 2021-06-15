using System;
using System.Linq;

namespace Adventure.Net.Actions
{
    public class Goto : Verb
    {
        public Goto()
        {
            Name = "goto";
        }

        public bool Expects()
        {
            Output.Print("Where do you want to go?");
            
            var x = CommandPrompt.GetInput();

            var room = (from r in Rooms.All
                       where r.GetType().Name.Contains(x)
                       select r).FirstOrDefault();
            
            if (room != null)
            {
                MovePlayer.To(room);
            }

            return true;
        }
    }
}
