using System;
using System.Linq;

namespace Adventure.Net
{
    public abstract class Direction : Verb
    {
        private Func<Room, Room> getRoom;

        protected void SetDirection(Func<Room, Room> mover, params string[] synonyms)
        {
            Name = synonyms.First();
            getRoom = mover;
            Synonyms.Are(synonyms);
        }

        public bool Expects()
        {
            var room = getRoom(Context.Story.Location);
            
            if (room == Context.Story.Location)
            {
                // do nothing
            }
            else if (room == null)
            {
                Print(Context.Story.Location.CantGo);
            }
            else
            {
                MovePlayer.To(room);
            }

            return true;
        }

        
    }
}
