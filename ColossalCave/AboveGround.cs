namespace ColossalCave
{
    public abstract class AboveGround : AdventRoom
    {
        protected AboveGround() 
        {
            HasLight = true;
            NoDwarf = true;
            IsStatic = true;
        }

        
    }
}
