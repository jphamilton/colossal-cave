namespace Adventure.Net.Actions
{
    public class Order : Verb
    {
        public Order()
        {
            Name = "order";
        }

        public bool Expects(Object obj)
        {
            if (obj.Animate)
            {
                return Print($"{obj.Article} {obj.Name} has better things to do.");
            }

            return Print(Messages.VerbNotRecognized);
        }
    }
}
