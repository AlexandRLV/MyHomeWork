using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PecmanOne
{
    class BLINKY: Enemies
    {
        public void SetTarget(MainHero hero)
        {
            this.targetx = hero.GetX();
            this.targety = hero.GetY();
        }
        
        public BLINKY(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.xprev = x;
            this.yprev = y;
            this.cornerx = -1;
            this.cornery = -1;
            this.color = ConsoleColor.Red;
        }
    }
}
