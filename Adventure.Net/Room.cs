using Adventure.Net.Actions;
using System;
using System.Collections.Generic;

namespace Adventure.Net
{
    public abstract class Room : Item
    {
        private List<Item> absent { get; } = new List<Item>();

        private readonly Dictionary<Type, Func<Direction, bool>> beforeMoveRoutines = new Dictionary<Type, Func<Direction, bool>>();
        public List<Item> Contents { get; private set; }

        protected Room() 
        {
            Contents = new List<Item>();
            Contents = new List<Item>();
            DarkToDark = () => Print("It's pitch black, and you can't see a thing.");
            CantGo = "You can't go that way.";
            Visited = false;
            Article = "";
        }

        public string CantGo { get; set; }

        public Action DarkToDark { get; set; }

        /// <summary>
        /// Called the first time a player enters the room
        /// </summary>
        public Action Initial { get; set; }

        /// <summary>
        /// Visited
        /// </summary>
        public bool Visited { get; set; }
        
        private readonly Dictionary<string, Func<Room>> roomMap = new Dictionary<string, Func<Room>>();

        private void AddToRoomMap<T>(string direction) where T:Room
        {
            Room room = Rooms.Get<T>();
            if (room != null)
            {
                roomMap.Remove(direction);
                roomMap.Add(direction, () => room);
            }
        }

        public void Before<T>(Func<Direction, bool> before) where T : IDirectionProxy
        {
            beforeMoveRoutines.Remove(typeof(T));
            beforeMoveRoutines.Add(typeof(T), before);
        }

        private void AddToRoomMap(string direction, Func<Room> getRoom)
        {
            roomMap.Remove(direction);
            roomMap.Add(direction, getRoom);
        }

        public void NorthTo<T>() where T : Room
        {
            AddToRoomMap<T>("n");
        }

        public void NorthTo(Func<Room> getRoom)
        {
            AddToRoomMap("n", getRoom);
        }

        public Room N()
        {
            return TryMove("n");
        }

        public void SouthTo<T>() where T : Room
        {
            AddToRoomMap<T>("s");
        }

        public void SouthTo(Func<Room> getRoom)
        {
            AddToRoomMap("s", getRoom);
        }

        public Room S()
        {
            return TryMove("s");
        }
        
        public void EastTo<T>() where T: Room
        {
            AddToRoomMap<T>("e");
        }

        public void EastTo(Func<Room> getRoom)
        {
            AddToRoomMap("e", getRoom);
        }

        public Room E()
        {
            return TryMove("e");
        }

        public void WestTo<T>() where T : Room
        {
            AddToRoomMap<T>("w");
        }

        public void WestTo(Func<Room> getRoom)
        {
            AddToRoomMap("w", getRoom);
        }

        public Room W()
        {
            return TryMove("w");
        }

        public void NorthWestTo<T>() where T : Room
        {
            AddToRoomMap<T>("nw");
        }

        public void NorthWestTo(Func<Room> getRoom)
        {
            AddToRoomMap("nw", getRoom);
        }

        public Room NW()
        {
            return TryMove("nw");
        }

        public void NorthEastTo<T>() where T : Room
        {
            AddToRoomMap<T>("ne");
        }

        public void NorthEastTo(Func<Room> getRoom)
        {
            AddToRoomMap("ne", getRoom);
        }

        public Room NE()
        {
            return TryMove("ne");
        }

        public void SouthWestTo<T>() where T : Room
        {
            AddToRoomMap<T>("sw");
        }

        public void SouthWestTo(Func<Room> getRoom)
        {
            AddToRoomMap("sw", getRoom);
        }

        public Room SW()
        {
            return TryMove("sw");
        }

        public void SouthEastTo<T>() where T : Room
        {
            AddToRoomMap<T>("se");
        }

        public void SouthEastTo(Func<Room> getRoom)
        {
            AddToRoomMap("se", getRoom);
        }

        public Room SE()
        {
            return TryMove("se");
        }

        public void InTo<T>() where T : Room
        {
            AddToRoomMap<T>("in");
        }

        public void InTo(Func<Room> getRoom)
        {
            AddToRoomMap("in", getRoom);
        }

        public Room IN()
        {
            return TryMove("in");
        }

        public void OutTo<T>() where T : Room
        {
            AddToRoomMap<T>("out");
        }

        public void OutTo(Func<Room> getRoom)
        {
            AddToRoomMap("out", getRoom);
        }

        public Room OUT()
        {
            return TryMove("out");
        }

        public void UpTo<T>() where T : Room
        {
            AddToRoomMap<T>("up");
        }

        public void UpTo(Func<Room> getRoom)
        {
            AddToRoomMap("up", getRoom);
        }

        public Room UP()
        {
            return TryMove("up");
        }

        public void DownTo<T>() where T : Room
        {
            AddToRoomMap<T>("down");
        }

        public void DownTo(Func<Room> getRoom)
        {
            AddToRoomMap("down", getRoom);
        }

        public Room DOWN()
        {
            return TryMove("down");
        }
        
        protected virtual Room HandleMove()
        {
            return this;    
        }

        protected virtual Room TryMove(string dir)
        {
            if (!roomMap.ContainsKey(dir))
                return null;

            var direction = (Direction)Verbs.Get(dir);
            var goType = typeof(Go);
            
            if (beforeMoveRoutines.ContainsKey(goType))
            {
                var before = beforeMoveRoutines[goType];

                if (before != null && before(direction))
                {
                    return null;
                }
            }

            var getRoom = roomMap[dir];
            var room = getRoom();

            if (room != null)
            {
                var beforeEnter = room.Before<Enter>();

                if (beforeEnter != null && beforeEnter())
                {
                    return CurrentRoom.Location;
                }

                room = room.HandleMove() ?? this;
            }
            
            return room;
        }

        public Item Has<T>() where T : Item
        {
            Item obj = Objects.Get<T>();
            
            if (obj == null)
            {
                obj = Rooms.Get(typeof(T));
            }

            Contents.Add(obj);
            
            return obj;
        }

        
        public bool Contains<T>() where T : Item
        {
            foreach(var obj in Contents)
            {
                if (obj is T)
                    return true;
            }

            return false;
        }

        public bool Contains(Item obj) 
        {
            return Contents.Contains(obj);
        }

    }
}