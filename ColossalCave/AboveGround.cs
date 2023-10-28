namespace ColossalCave;

public abstract class AboveGround : BelowGround
{
    protected AboveGround()
    {
        Light = true;
        NoDwarf = true;
        Static = true;
    }


}
