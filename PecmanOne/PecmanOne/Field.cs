using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PecmanOne
{
    class Field
    {
        private int maxx, maxy;
        public char[,] field;
        public ConsoleColor[,] fieldcolor;
        private int[,] obstacles;
        private char[] obstacle;
        private ConsoleColor[] obstaclecolor;
        private int obstaclenum;
        private int pointnum;
        private int[,] points;
        private int livesnum;
        private int[,] lives;
        private int energynum;
        private int[,] energyes;
        char energy = '$';
        char live = '@';
        char point = '°';

        public Field(int x, int y)
        {           
            this.field = new char[x, y];
            this.maxx = x;
            this.maxy = y;
            this.obstacles = new int[x * y, 2];
            this.obstacle = new char[x * y];
            this.obstaclecolor = new ConsoleColor[x * y];
            this.obstaclenum = 0;
            this.pointnum = 0;
            this.points = new int[x * y, 2];
            this.lives = new int[x * y, 2];
            this.fieldcolor = new ConsoleColor[x, y];
            this.energyes = new int[x * y,2];
            this.energynum = 0;
            ClearField();
        }

        public void WriteField(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write('┌');
            for (int i = 0; i < this.maxy; i++)
            {
                Console.Write("──");
            }
            Console.Write('┐');
            Console.WriteLine();
            Console.ResetColor();
            for (int i = 0; i < this.maxx; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write('│');
                for (int j = 0; j < this.maxy; j++)
                {
                    Console.ForegroundColor = fieldcolor[i, j];
                    Console.Write(this.field[i, j]);
                    Console.Write(' ');
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write('│');
                Console.WriteLine();
            }
            Console.Write('└');
            for (int i = 0; i < this.maxy; i++)
            {
                Console.Write("──");
            }
            Console.Write('┘');
            Console.ResetColor();
        }

        public void ClearField()
        {
            for (int i = 0; i < this.maxx; i++)
            {
                for (int j = 0; j < this.maxy; j++)
                {
                    this.field[i, j] = ' ';
                }
            }
            this.WriteObstacles();
            this.WritePoints();
            this.WriteLives();
            this.WriteEnergy();
        }
        
        public bool IsClear(int x, int y)
        {
            if ((x < this.maxx) && (x >= 0) && (y < this.maxy) && (y >= 0))
            {
                if ((this.field[x, y] != '■'))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void CreateObstacle(char o, int x, int y, ConsoleColor color)
        {
            if (this.IsClear(x, y))
            {
                int n = this.obstaclenum;
                this.obstacles[n, 0] = x;
                this.obstacles[n, 1] = y;
                this.obstaclecolor[n] = color;
                this.obstacle[n] = o;
                this.obstaclenum++;
                this.field[x, y] = o;
                this.fieldcolor[x, y] = color;
            }
        }

        public void CreateLives(int x, int y)
        {
            if (this.IsClear(x, y))
            {
                int n = this.livesnum;
                this.lives[n, 0] = x;
                this.lives[n, 1] = y;
                this.livesnum++;
                this.field[x, y] = this.live;
                this.fieldcolor[x, y] = ConsoleColor.DarkRed;
            }
        }

        public void CreateEnergy(int x, int y)
        {
            if (this.IsClear(x, y))
            {
                int n = this.energynum;
                this.energyes[n, 0] = x;
                this.energyes[n, 1] = y;
                this.energynum++;
                this.field[x, y] = this.energy;
                this.fieldcolor[x, y] = ConsoleColor.Cyan;
            }
        }

        public void CreatePoint(int x,int y)
        {
            if (this.IsClear(x, y))
            {
                int n = this.pointnum;
                this.points[n, 0] = x;
                this.points[n, 1] = y;
                this.pointnum++;
                this.field[x, y] = this.point;
                this.fieldcolor[x, y] = ConsoleColor.DarkYellow;
            }
        }

        private void WriteObstacles()
        {
            int n = this.obstaclenum;
            for (int i = 0; i < n; i++)
            {
                int x = this.obstacles[i, 0];
                int y = this.obstacles[i, 1];
                this.field[x, y] = this.obstacle[i];
                this.fieldcolor[x, y] = this.obstaclecolor[i];
            }
        }

        private void WritePoints()
        {
            int n = this.pointnum;
            for (int i = 0; i < n; i++)
            {
                int x = this.points[i, 0];
                int y = this.points[i, 1];
                this.field[x, y] = this.point;
                this.fieldcolor[x, y] = ConsoleColor.DarkYellow;
            }
        }

        private void WriteLives()
        {
            int n = this.livesnum;
            for (int i = 0; i < n; i++)
            {
                int x = this.lives[i, 0];
                int y = this.lives[i, 1];
                this.field[x, y] = this.live;
                this.fieldcolor[x, y] = ConsoleColor.DarkRed;
            }
        }

        private void WriteEnergy()
        {
            int n = this.energynum;
            for (int i = 0; i < n; i++)
            {
                int x = this.energyes[i, 0];
                int y = this.energyes[i, 1];
                this.field[x, y] = this.energy;
                this.fieldcolor[x, y] = ConsoleColor.DarkCyan;
            }
        }

        public void DeletePoint(int x,int y)
        {
            int n = this.pointnum;
            int i = 0;

            while (i<n)
            {
                if ((this.points[i,0]==x)&&(this.points[i,1]==y))
                {
                    break;
                }
                i++;
            }
            for (int j=i;j<n-1;j++)
            {
                this.points[j, 0] = this.points[j + 1, 0];
                this.points[j, 1] = this.points[j + 1, 1];
            }
            this.pointnum--;
            this.field[x, y] = ' ';
        }

        public void DeleteLive(int x, int y)
        {
            int n = this.livesnum;
            int i = 0;

            while (i < n)
            {
                if ((this.lives[i, 0] == x) && (this.lives[i, 1] == y))
                {
                    break;
                }
                i++;
            }
            for (int j = i; j < n - 1; j++)
            {
                this.lives[j, 0] = this.lives[j + 1, 0];
                this.lives[j, 1] = this.lives[j + 1, 1];
            }
            this.livesnum--;
            this.field[x, y] = ' ';
        }

        public void DeleteEnergy(int x, int y)
        {
            int n = this.energynum;
            int i = 0;

            while (i < n)
            {
                if ((this.energyes[i, 0] == x) && (this.energyes[i, 1] == y))
                {
                    break;
                }
                i++;
            }
            for (int j = i; j < n - 1; j++)
            {
                this.energyes[j, 0] = this.energyes[j + 1, 0];
                this.energyes[j, 1] = this.energyes[j + 1, 1];
            }
            this.energynum--;
            this.field[x, y] = ' ';
        }

        public bool ArePoints()
        {
            if (this.pointnum>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsPoint(int x,int y)
        {
            if ((x < this.maxx) && (x >= 0) && (y < this.maxy) && (y >= 0))
            {
                if ((this.field[x, y] == this.point))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool IsLive(int x, int y)
        {
            if ((x < this.maxx) && (x >= 0) && (y < this.maxy) && (y >= 0))
            {
                if ((this.field[x, y] == this.live))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool IsEnergy(int x, int y)
        {
            if ((x < this.maxx) && (x >= 0) && (y < this.maxy) && (y >= 0))
            {
                if ((this.field[x, y] == this.energy))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
