namespace Adventure.Net
{
    public class Player 
    {
        public Player() 
        {
        }

        public static bool Has<T>() where T:Item
        {
            return Inventory.Contains<T>();
        }

        
    }
}
