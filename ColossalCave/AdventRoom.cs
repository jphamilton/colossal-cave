using System;
using Adventure.Net;

namespace ColossalCave
{
    /// <summary>
    /// Base class for Room (as far as *this* game is concerned)
    /// </summary>
    public abstract class AdventRoom : Room
    {
        private static bool darkWarning;

        protected AdventRoom() 
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
                        var rnd = new Random();
                        if (rnd.Next(1, 4) == 1)
                        {
                            //TODO: dead = true; ????
                            Print("You fell into a pit and broke every bone in your body!");
                        }    
                    }
                    
                };           
        }

        public bool NoDwarf { get; set; }
    }
}

