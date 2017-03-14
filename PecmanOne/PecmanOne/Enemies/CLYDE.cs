using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PecmanOne
{
    class CLYDE: Enemies
    {
        public CLYDE(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.xstart = x;
            this.ystart = y;
            this.xprev = x;
            this.yprev = y;
            this.cornerx = 30;
            this.cornery = 40;
            this.color = ConsoleColor.White;
            this.canmove = false;
        }

        public void SetTarget(MainHero hero)
        {
            double d = Math.Sqrt((this.x - hero.GetX()) * (this.x - hero.GetX()) + (this.y - hero.GetY()) * (this.y - hero.GetY()));
            if (d>8)
            {
                this.targetx = hero.GetX();
                this.targety = hero.GetY();
            }
            else
            {
                this.SetTargetToCorner();
            }
        }
    }
}
