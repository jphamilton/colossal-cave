using System;
using System.Linq.Expressions;

namespace Adventure.Net
{
    public abstract class DirectionalVerb : Verb
    {
        private Func<Room, Room> getRoom;

        protected void SetDirection(Expression<Func<Room, Room>> x, params string[] synonyms)
        {
            getRoom = x.Compile();
            Synonyms.Are(synonyms);
            Grammars.Add(Grammar.Empty, MovePlayer);
        }

        protected bool MovePlayer()
        {
            var L = new Library();
            var room = getRoom(Context.Story.Location);
            if (room == null)
                Print(Context.Story.Location.CantGo);
            else
                L.MovePlayerTo(room);
            return true;
        }

        
    }
}
