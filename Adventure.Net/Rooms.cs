namespace Adventure.Net;

public static class Rooms
{
    public static T Get<T>() where T : Room
    {
        return Objects.Get<T>();
    }

}
