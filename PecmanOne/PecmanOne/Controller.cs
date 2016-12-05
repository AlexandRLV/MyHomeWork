using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PecmanOne
{
    class Controller
    {
        private int x, y, level;

        public Controller(int level)
        {
            this.x = 28;
            this.y = 31;
            if (level<5)
            {
                this.level = 150;
            }
            else if (level<10)
            {
                this.level = 100;
            }
            else if (level<15)
            {
                this.level = 75;
            }
            else if (level <20)
            {
                this.level = 50;
            }
            else
            {
                this.level = 30;
            }
        }

        public void WriteLevel(Field f)
        {
            char c;
            StreamReader file = new StreamReader(@"Field.txt");
            for (int i = 0; i < 31; i++)
            {
                for (int j = 0; j < 28; j++)
                {
                    c = Convert.ToChar(file.Read());
                    if (c == '■')
                    {
                        f.CreateObstacle(c, j, i, ConsoleColor.DarkBlue);
                    }
                    else if (c=='.')
                    {
                        f.CreatePoint(j, i);
                    }
                    else if (c== '@')
                    {
                        f.CreateLives(j, i);
                    }
                }
                file.ReadLine();
            }
        }

        public void Start()
        {
            Console.CursorVisible = false;
            Field f = new Field(this.x, this.y);
            WriteLevel(f);
            MainHero hero = new MainHero(13, 23);
            hero.SetScore(0);
            BLINKY blinky = new BLINKY(13, 11);
            PINKY pinky = new PINKY(13, 14);
            INKY inky = new INKY(14, 14);
            CLYDE clyde = new CLYDE(12, 14);
            if (this.level>50)
            {
                hero.SetLives(3);
            }
            else
            {
                hero.SetLives(2);
            }
            Console.SetCursorPosition(65, 0);
            Console.Write("Press arrows to move, press escape to exit.");
            Console.SetCursorPosition(65, 1);
            Console.Write("Points:");
            Console.SetCursorPosition(65, 2);
            Console.Write("Lives:");
            Console.SetCursorPosition(0, 0);
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.RightArrow)
                    {
                        hero.SetSpeed(0, 1);
                        hero.SetDirection(0);
                    }
                    else if (key.Key == ConsoleKey.LeftArrow)
                    {
                        hero.SetSpeed(0, -1);
                        hero.SetDirection(2);
                    }
                    else if (key.Key == ConsoleKey.DownArrow)
                    {
                        hero.SetSpeed(1, 0);
                        hero.SetDirection(1);
                    }
                    else if (key.Key == ConsoleKey.UpArrow)
                    {
                        hero.SetSpeed(-1, 0);
                        hero.SetDirection(3);
                    }
                    else if (key.Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                }
                hero.Moving(f);
                blinky.SetTarget(hero);
                blinky.MoveToTarget(f);
                blinky.WriteOnField(f);
                pinky.SetTarget(hero);
                pinky.MoveToTarget(f);
                pinky.WriteOnField(f);
                inky.SetTarget(hero, blinky);
                inky.MoveToTarget(f);
                inky.WriteOnField(f);
                clyde.SetTarget(hero);
                clyde.MoveToTarget(f);
                clyde.WriteOnField(f);
                hero.WriteOnField(f);
                f.WriteField();
                Console.SetCursorPosition(73, 1);
                Console.WriteLine("{0}", hero.GetScore());
                Console.SetCursorPosition(73, 2);
                Console.WriteLine("{0}", hero.GetLives());
                if (!f.ArePoints())
                {
                    break;
                }
                Console.SetCursorPosition(0, 0);
                Thread.Sleep(level);
                f.ClearField();
            }
        }
    }
}
