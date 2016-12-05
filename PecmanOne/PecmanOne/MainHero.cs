using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PecmanOne
{
    class MainHero
    {
        private int x, y;
        private int score;
        private int lives;
        private int speedx;
        private int speedy;
        private int direction;
        ConsoleColor color = new ConsoleColor();

        public int GetX()
        {
            return this.x;
        }

        public int GetY()
        {
            return this.y;
        }

        public int GetDirection()
        {
            return this.direction;
        }

        public int GetScore()
        {
            return this.score;
        }

        public void SetScore(int x)
        {
            this.score = x;
        }

        public int GetLives()
        {
            return this.lives;
        }

        public void SetLives(int x)
        {
            this.lives = x;
        }

        public MainHero(int x, int y)
        {
            this.x = x;
            this.y = y;
            color = ConsoleColor.Green;
        }

        public void SetSpeed(int speedx, int speedy)
        {
            this.speedx = speedx;
            this.speedy = speedy;
        }

        public void SetDirection(int d)
        {
            this.direction = d;
        }

        public void WriteOnField(Field f)
        {
            f.field[this.x, this.y] = 'O';
            f.fieldcolor[this.x, this.y] = this.color;
        }

        public void Moving(Field f)
        {
            if (f.IsClear(this.x + this.speedx, this.y + this.speedy))
            {
                this.x += this.speedx;
                this.y += this.speedy;
            }
            if (this.y==14)
            {
                if (this.x == 0)
                {
                    this.x = 27;
                }
                else if (this.x==27)
                {
                    this.x = 0;
                }
            }
            
            if (f.IsPoint(this.x,this.y))
            {
                this.score++;
                f.DeletePoint(this.x, this.y);
            }
            if (f.IsLive(this.x, this.y))
            {
                this.lives++;
                f.DeleteLive(this.x, this.y);
            }
        }
    }
}
