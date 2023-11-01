namespace Adventure.Net;

public static class Move<O> where O : Object
{
    public static void To<R>() where R : Room
    {
        var obj = Objects.Get<O>();
        obj.Remove();

        var room = Rooms.Get<R>();

        ObjectMap.Add(obj, room);
    }
}
