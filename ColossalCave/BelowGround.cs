using Adventure.Net;
using Adventure.Net.Utilities;
using ColossalCave.Places;

namespace ColossalCave
{
    /// <summary>
    /// Base class for Room (as far as *this* game is concerned)
    /// </summary>
    public abstract class BelowGround : Room
    {
        private static bool darkWarning;

        protected BelowGround() 
        {
            // we generally allow dwarves and their ilk to freely roam at will
            NoDwarf = false;

            // dark by default
            HasLight = false;

            DarkToDark = () =>
                {
                    if (!darkWarning)
                    {
                        darkWarning = true;
                        Print("It is now pitch dark. If you proceed you will likely fall into a pit.");
                    }
                    else
                    {
                        if (Random.Number(1, 4) == 1)
                        {
                            Print("You fell into a pit and broke every bone in your body!");
                            AfterLife.GoTo();
                        }    
                    }
                    
                };           
        }

        public bool NoDwarf { get; set; }
    }
}

