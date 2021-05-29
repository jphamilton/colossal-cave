using System;
using System.Text;
using Adventure.Net.Verbs;

namespace Adventure.Net
{
    public abstract class Door : Room 
    {
        protected Door()
        {
            IsStatic = true;
            IsOpenable = true;
        }

        public Func<Room> DoorTo { get; set; }

        public Func<DirectionalVerb> DoorDirection { get; set; } 
        
        protected T Direction<T>() where T:DirectionalVerb
        {
            Type t = typeof (T);
            return Activator.CreateInstance(t) as T;
        }

        protected override Room HandleMove()
        {
            if (!IsLocked && IsOpen)
                return DoorTo();

            if (DoorDirection() is Down)
                Print($"You are unable to descend by the {Name}.");
            else if (DoorDirection() is Up)
                Print($"You are unable to ascend by the {Name}");
            else if (DoorDirection() != null)
                Print("You can't go that way.");
            else
            {
                string lead = HasPluralName ? "leads" : "lead";
                Print($"You can't since the {Name} {lead} to nowhere.");
            }

            return null;

        }
    }
}

