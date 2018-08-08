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
            IsLockable = true;
        }

        public Object Key { get; private set; }
        
        public Func<Room> DoorTo { get; set; }

        public Func<DirectionalVerb> DoorDirection { get; set; } 
        
        public void WithKey<T>() where T:Object
        {
            Key = Net.Objects.Get<T>();            
        }

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
                Print("You are unable to descend by the {0}.", Name);
            else if (DoorDirection() is Up)
                Print("You are unable to ascend by the {0}", Name);
            else if (DoorDirection() != null)
                Print("You can't go that way.");
            else
            {
                string lead = HasPluralName ? "leads" : "lead";
                Print("You can't since the {0} {1} to nowhere.", Name, lead);
            }

            return null;

        }

        //public string Unlock()
        //{
        //    return Lock(false);
        //}

        //public string Lock()
        //{
        //    return Lock(true);
        //}

        //private string Lock(bool locked)
        //{
        //    string action = locked ? "lock" : "unlock";
        //    StringBuilder sb = new StringBuilder();

        //    if (Inventory.Contains(Key))
        //    {
        //        if (Context.IndirectObject != Key && Inventory.Objects.Count == 1)
        //            sb.AppendFormat("(with the {0})\n", Key.Name);
        //        sb.AppendFormat("You {0} the {1}.", action, Name);
        //        IsLocked = locked;
        //    }
        //    else
        //    {
        //        sb.AppendFormat("You have nothing to {0} that with.", action);
        //    }

        //    return sb.ToString();
        //}


    }
}

