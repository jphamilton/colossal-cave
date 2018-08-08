namespace Adventure.Net
{
    public class Noun
    {
        public static bool Is<T>() where T:Object
        {
            Object obj = Objects.Get<T>();
            return Context.Object == obj;
        }
    }
}
