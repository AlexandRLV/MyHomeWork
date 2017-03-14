using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PecmanOne
{
    class Enemies
    {
        public int x;
        public int y;
        public int xstart;
        public int ystart;
        public int xprev;
        public int yprev;
        public int targetx;
        public int targety;
        public int cornerx;
        public int cornery;
        public bool canmove;
        public ConsoleColor color;

        public void MoveToTarget(Field f)
        {
            if (!canmove)
            {
                return;
            }
            int x, y;
            double[] d = new double[4];
            x = this.x - 1;
            y = this.y;
            if ((f.IsClear(x,y))&& ((x != this.xprev) || (y != this.yprev)))
            {
                d[0] = Math.Sqrt((this.targetx - x) * (this.targetx - x) + (this.targety - y) * (this.targety - y));
            }
            else
            {
                d[0] = 10000;
            }
            x = this.x + 1;
            y = this.y;
            if ((f.IsClear(x, y)) && ((x != this.xprev) || (y != this.yprev)))
            {
                d[1] = Math.Sqrt((this.targetx - x) * (this.targetx - x) + (this.targety - y) * (this.targety - y));
            }
            else
            {
                d[1] = 10000;
            }
            x = this.x;
            y = this.y - 1;
            if ((f.IsClear(x, y)) && ((x != this.xprev) || (y != this.yprev)))
            {
                d[2] = Math.Sqrt((this.targetx - x) * (this.targetx - x) + (this.targety - y) * (this.targety - y));
            }
            else
            {
                d[2] = 10000;
            }
            x = this.x;
            y = this.y + 1;
            if ((f.IsClear(x, y)) && ((x != this.xprev) || (y != this.yprev)))
            {
                d[3] = Math.Sqrt((this.targetx - x) * (this.targetx - x) + (this.targety - y) * (this.targety - y));
            }
            else
            {
                d[3] = 10000;
            }
            this.xprev = this.x;
            this.yprev = this.y;
            double min = 10000;
            int m = -1;
            for (int i=0;i<4;i++)
            {
                if (d[i]<min)
                {
                    min = d[i];
                    m = i;
                }
            }
            if (m==0)
            {
                this.x -= 1;
            }
            else if (m==1)
            {
                this.x += 1;
            }
            else if (m==2)
            {
                this.y -= 1;
            }
            else if (m==3)
            {
                this.y += 1;
            }
            else
            {
                this.x = this.xprev;
                this.y = this.yprev;
            }
        }

        public void MoveAwayFromTarget(Field f)
        {
            if (!canmove)
            {
                return;
            }
            int x, y;
            double[] d = new double[4];
            x = this.x - 1;
            y = this.y;
            if ((f.IsClear(x, y)) && ((x != this.xprev) || (y != this.yprev)))
            {
                d[0] = Math.Sqrt((this.targetx - x) * (this.targetx - x) + (this.targety - y) * (this.targety - y));
            }
            else
            {
                d[0] = -10000;
            }
            x = this.x + 1;
            y = this.y;
            if ((f.IsClear(x, y)) && ((x != this.xprev) || (y != this.yprev)))
            {
                d[1] = Math.Sqrt((this.targetx - x) * (this.targetx - x) + (this.targety - y) * (this.targety - y));
            }
            else
            {
                d[1] = -10000;
            }
            x = this.x;
            y = this.y - 1;
            if ((f.IsClear(x, y)) && ((x != this.xprev) || (y != this.yprev)))
            {
                d[2] = Math.Sqrt((this.targetx - x) * (this.targetx - x) + (this.targety - y) * (this.targety - y));
            }
            else
            {
                d[2] = -10000;
            }
            x = this.x;
            y = this.y + 1;
            if ((f.IsClear(x, y)) && ((x != this.xprev) || (y != this.yprev)))
            {
                d[3] = Math.Sqrt((this.targetx - x) * (this.targetx - x) + (this.targety - y) * (this.targety - y));
            }
            else
            {
                d[3] = -10000;
            }
            this.xprev = this.x;
            this.yprev = this.y;
            double min = -10000;
            int m = -1;
            for (int i = 0; i < 4; i++)
            {
                if (d[i] > min)
                {
                    min = d[i];
                    m = i;
                }
            }
            if (m == 0)
            {
                this.x -= 1;
            }
            else if (m == 1)
            {
                this.x += 1;
            }
            else if (m == 2)
            {
                this.y -= 1;
            }
            else if (m == 3)
            {
                this.y += 1;
            }
            else
            {
                this.x = this.xprev;
                this.y = this.yprev;
            }
        }

        public void CanMove()
        {
            this.canmove = true;
        }

        public void WriteOnField(Field f)
        {
            f.field[this.x, this.y] = '®';
            f.fieldcolor[this.x, this.y] = this.color;
        }

        public void DeleteFromField(Field f)
        {
            if (f.IsEnergy(this.x,this.y))
            {
                f.field[this.x, this.y] = '$';
            }
            else if (f.IsLive(this.x,this.y))
            {
                f.field[this.x, this.y] = '@';
            }
            else if (f.IsPoint(this.x,this.y))
            {
                f.field[this.x, this.y] = '°';
            }
            else
            {
                f.field[this.x, this.y] = ' ';
            }            
        }

        public void SetTargetToCorner()
        {
            this.targetx = this.cornerx;
            this.targety = this.cornery;
            this.xprev = this.x;
            this.yprev = this.y;
        }

        public bool IsCatched(MainHero hero)
        {
            if ((hero.GetX() == this.x)&&(hero.GetY()==this.y))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool WasCatched(MainHero hero)
        {
            if ((hero.GetX() == this.x) && (hero.GetY() == this.y))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void WasCatchedBySuperHero()
        {
            this.x = this.xstart;
            this.y = this.ystart;
        }
    }
}
