namespace ColossalCave
{
    public abstract class AboveGround : BelowGround
    {
        protected AboveGround() 
        {
            HasLight = true;
            NoDwarf = true;
            Static = true;
        }

        
    }
}
