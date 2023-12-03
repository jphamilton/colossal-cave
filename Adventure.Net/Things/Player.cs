namespace Adventure.Net.Things;

public class Player : Object
{
    private static Player _player;

    public override void Initialize()
    {
        _player = Objects.Get<Player>();
    }

    public static new Room Location
    {
        get
        {
            return (Room)_player.Parent;
        }
        set
        {
            _player.Parent = value;
        }
    }

    public static bool IsCarrying<T>() where T : Object => Inventory.Contains<T>();

    public static bool IsCarrying(Object obj) => Inventory.Contains(obj);
}
