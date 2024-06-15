using Adventure.Net.ActionRoutines;
using Adventure.Net.Things;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System;

namespace Adventure.Net;

[DebuggerDisplay("{Name,nq}")]
public abstract class Room : Object
{
    private readonly Dictionary<Type, Func<Room>> roomMap = [];
    private readonly Dictionary<Type, Func<Direction, bool>> beforeMoveActions = [];

    protected Room()
    {
        DarkToDark = () => Print("It's pitch black, and you can't see a thing.");
        CantGo = "You can't go that way.";
        Visited = false;
        DefiniteArticle = "";
    }

    /// <summary>
    /// Room has been visited
    /// </summary>
    public bool Visited { get; set; }

    [JsonIgnore]
    public string CantGo { get; set; }

    /// <summary>
    /// Routine that is called when player is moving in darkness
    /// </summary>
    /// <remarks>Should return false if player has died</remarks>
    public Func<bool> DarkToDark { get; set; }

    /// <summary>
    /// Called the first time a player enters the room
    /// </summary>
    public Action Initial { get; set; }

    private void AddToRoomMap<T, D>()
        where T : Room
        where D : Direction
    {
        Room room = Rooms.Get<T>();

        if (room != null)
        {
            roomMap.Remove(typeof(D));
            roomMap.Add(typeof(D), () => room);
        }
    }

    private void AddToRoomMap<D>(Func<Room> getRoom) where D : Direction
    {
        roomMap.Remove(typeof(D));
        roomMap.Add(typeof(D), getRoom);
    }

    public void Before<T>(Func<Direction, bool> before) where T : Direction
    {
        beforeMoveActions.Remove(typeof(T));
        beforeMoveActions.Add(typeof(T), before);
    }

    public void NorthTo<T>() where T : Room
    {
        AddToRoomMap<T, North>();
    }

    public void NorthTo(Func<Room> getRoom)
    {
        AddToRoomMap<North>(getRoom);
    }

    public void SouthTo<T>() where T : Room
    {
        AddToRoomMap<T, South>();
    }

    public void SouthTo(Func<Room> getRoom)
    {
        AddToRoomMap<South>(getRoom);
    }

    public void EastTo<T>() where T : Room
    {
        AddToRoomMap<T, East>();
    }

    public void EastTo(Func<Room> getRoom)
    {
        AddToRoomMap<East>(getRoom);
    }

    public void WestTo<T>() where T : Room
    {
        AddToRoomMap<T, West>();
    }

    public void WestTo(Func<Room> getRoom)
    {
        AddToRoomMap<West>(getRoom);
    }

    public void NorthWestTo<T>() where T : Room
    {
        AddToRoomMap<T, Northwest>();
    }

    public void NorthWestTo(Func<Room> getRoom)
    {
        AddToRoomMap<Northwest>(getRoom);
    }

    public void NorthEastTo<T>() where T : Room
    {
        AddToRoomMap<T, Northeast>();
    }

    public void NorthEastTo(Func<Room> getRoom)
    {
        AddToRoomMap<Northeast>(getRoom);
    }

    public void SouthWestTo<T>() where T : Room
    {
        AddToRoomMap<T, Southwest>();
    }

    public void SouthWestTo(Func<Room> getRoom)
    {
        AddToRoomMap<Southwest>(getRoom);
    }

    public void SouthEastTo<T>() where T : Room
    {
        AddToRoomMap<T, Southeast>();
    }

    public void SouthEastTo(Func<Room> getRoom)
    {
        AddToRoomMap<Southeast>(getRoom);
    }

    public void InTo<T>() where T : Room
    {
        AddToRoomMap<T, In>();
    }

    public void InTo(Func<Room> getRoom)
    {
        AddToRoomMap<In>(getRoom);
    }

    public void OutTo<T>() where T : Room
    {
        AddToRoomMap<T, Out>();
    }

    public void OutTo(Func<Room> getRoom)
    {
        AddToRoomMap<Out>(getRoom);
    }

    public void UpTo<T>() where T : Room
    {
        AddToRoomMap<T,Up>();
    }

    public void UpTo(Func<Room> getRoom)
    {
        AddToRoomMap<Up>(getRoom);
    }

    public void DownTo<T>() where T : Room
    {
        AddToRoomMap<T,Down>();
    }

    public void DownTo(Func<Room> getRoom)
    {
        AddToRoomMap<Down>(getRoom);
    }

    protected virtual Room HandleMove()
    {
        return this;
    }

    public virtual Room TryMove<T>() where T : Direction
    {
        var direction = Routines.Get<T>();
        return TryMove(direction);
    }

    public virtual Room TryMove<T>(T direction) where T : Direction
    {
        if (!roomMap.ContainsKey(typeof(T)))
        {
            return null;
        }

        var goType = typeof(Go);

        if (beforeMoveActions.ContainsKey(goType))
        {
            var before = beforeMoveActions[goType];

            if (before != null && before(direction))
            {
                return this;
            }
        }

        var getRoom = roomMap[typeof(T)];

        var room = getRoom();

        if (room != null)
        {
            if (room is Door door)
            {
                if (door.Open)
                {
                    room = door.DoorTo();
                }
            }
            else
            {
                var beforeEnter = room.GetBeforeRoutine(typeof(Enter));

                if (beforeEnter != null && beforeEnter())
                {
                    return Player.Location;
                }
            }

            room = room.HandleMove() ?? this;
        }

        return room;
    }

    public Object Add<T>() where T : Object
    {
        Object obj = Objects.Get<T>();
        obj.MoveToLocation();
        return obj;
    }
}