using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PecmanOne
{
    class PINKY: Enemies
    {
        public PINKY(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.xprev = x;
            this.yprev = y;
            this.cornerx = 30;
            this.cornery = -1;
            this.color = ConsoleColor.Magenta;
        }

        public void SetTarget(MainHero hero)
        {
            int d = hero.GetDirection();
            if (d==0)
            {
                this.targetx = hero.GetX();
                this.targety = hero.GetY() + 4;
            }
            else if (d == 1)
            {
                this.targetx = hero.GetX() + 4;
                this.targety = hero.GetY();
            }
            else if (d == 2)
            {
                this.targetx = hero.GetX();
                this.targety = hero.GetY() - 4;
            }
            else if (d == 3)
            {
                this.targetx = hero.GetX() - 4;
                this.targety = hero.GetY();
            }
        }
    }
}
