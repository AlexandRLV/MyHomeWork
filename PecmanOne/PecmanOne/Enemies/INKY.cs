using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PecmanOne
{
    class INKY: Enemies
    {
        public INKY(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.xstart = x;
            this.ystart = y;
            this.xprev = x;
            this.yprev = y;
            this.cornerx = -1;
            this.cornery = 40;
            this.color = ConsoleColor.Blue;
            this.canmove = false;
        }

        public void SetTarget(MainHero hero, BLINKY b)
        {
            int x=0, y=0;
            int d = hero.GetDirection();
            if (d == 0)
            {
                x = hero.GetX();
                y = hero.GetY() + 2;
            }
            else if (d == 1)
            {
                x = hero.GetX() + 2;
                y = hero.GetY();
            }
            else if (d == 2)
            {
                x = hero.GetX();
                y = hero.GetY() - 2;
            }
            else if (d == 3)
            {
                x = hero.GetX() - 2;
                y = hero.GetY();
            }

            this.targetx = x + x - b.x;
            this.targety = y + y - b.y;
        }
    }
}
