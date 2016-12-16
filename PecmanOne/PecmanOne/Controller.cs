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
        private int x, y, level, levelspeed, time;

        public Controller(int level)
        {
            this.x = 28;
            this.y = 31;
            this.level = level;
            if (level<15)
            {
                this.levelspeed = 75;
            }
            else
            {
                this.levelspeed = 30;
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
                    else if (c == '$')
                    {
                        f.CreateEnergy(j, i);
                    }
                }
                file.ReadLine();
            }
            file.Close();
        }

        public void Start()
        {
            Console.CursorVisible = false;
            Field f = new Field(this.x, this.y);
            WriteLevel(f);
            MainHero hero = new MainHero(13, 23);
            hero.SetScore(0);
            if (this.level<5)
            {
                hero.SetSuperCounter(10);
            }
            else if (this.level<10)
            {
                hero.SetSuperCounter(8);
            }
            else if (this.level<15)
            {
                hero.SetSuperCounter(6);
            }
            else if (this.level<20)
            {
                hero.SetSuperCounter(4);
            }
            else
            {
                hero.SetSuperCounter(2);
            }
            BLINKY blinky = new BLINKY(13, 11);
            PINKY pinky = new PINKY(13, 13);
            INKY inky = new INKY(14, 15);
            CLYDE clyde = new CLYDE(12, 15);
            if (this.levelspeed>50)
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
            Console.SetCursorPosition(65, 3);
            Console.Write("Time:");
            Console.SetCursorPosition(0, 0);
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            int counter = 0;
            bool isgoingtocorner = false;
            this.time = 0;
            for (int i=0;i<3;i++)
            {
                Console.SetCursorPosition(32, 14);
                Console.WriteLine("{0}", 3 - i);
                Thread.Sleep(500);
            }
            Console.SetCursorPosition(32, 14);
            Console.WriteLine("GO!");
            Thread.Sleep(1000);
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.RightArrow)
                    {
                        if (f.IsClear(hero.GetX(),hero.GetY()+1))
                        {
                            hero.SetSpeed(0, 1);
                            hero.SetDirection(0);
                        }
                    }
                    else if (key.Key == ConsoleKey.LeftArrow)
                    {
                        if (f.IsClear(hero.GetX(), hero.GetY() - 1))
                        {
                            hero.SetSpeed(0, -1);
                            hero.SetDirection(2);
                        }                            
                    }
                    else if (key.Key == ConsoleKey.DownArrow)
                    {
                        if (f.IsClear(hero.GetX() + 1, hero.GetY()))
                        {
                            hero.SetSpeed(1, 0);
                            hero.SetDirection(1);
                        }                            
                    }
                    else if (key.Key == ConsoleKey.UpArrow)
                    {
                        if (f.IsClear(hero.GetX() - 1, hero.GetY()))
                        {
                            hero.SetSpeed(-1, 0);
                            hero.SetDirection(3);
                        }                            
                    }
                    else if (key.Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                }
                hero.Moving(f);
                if (hero.GetScore()>30)
                {
                    inky.CanMove();
                }
                if (hero.GetScore()>100)
                {
                    clyde.CanMove();
                }
                if (((counter>=0)&&(counter<10))||((counter>=40)&&(counter<=50)) || ((counter >= 80) && (counter <= 90)))
                {
                    if (!isgoingtocorner)
                    {
                        blinky.SetTargetToCorner();
                        pinky.SetTargetToCorner();
                        inky.SetTargetToCorner();
                        clyde.SetTargetToCorner();
                        isgoingtocorner = true;
                    }                    
                    counter++;
                }
                else
                {
                    blinky.SetTarget(hero);
                    pinky.SetTarget(hero);
                    inky.SetTarget(hero, blinky);
                    clyde.SetTarget(hero);
                    isgoingtocorner = false;
                    counter++;
                }
                if (hero.SuperMode())
                {                    
                    counter = 10;
                    blinky.MoveAwayFromTarget(f);
                    blinky.WriteOnField(f);
                    pinky.MoveAwayFromTarget(f);
                    pinky.WriteOnField(f);
                    inky.MoveAwayFromTarget(f);
                    inky.WriteOnField(f);
                    clyde.MoveAwayFromTarget(f);
                    clyde.WriteOnField(f);
                    if (blinky.WasCatched(hero))
                    {
                        hero.AddScore(100);
                        blinky.WasCatchedBySuperHero();
                        counter = 0;
                    }
                    if (pinky.WasCatched(hero))
                    {
                        hero.AddScore(100);
                        pinky.WasCatchedBySuperHero();
                        counter = 0;
                    }
                    if (inky.WasCatched(hero))
                    {
                        hero.AddScore(100);
                        inky.WasCatchedBySuperHero();
                        counter = 0;
                    }
                    if (clyde.WasCatched(hero))
                    {
                        hero.AddScore(100);
                        clyde.WasCatchedBySuperHero();
                        counter = 0;
                    }
                    Console.SetCursorPosition(65, 4);
                    Console.Write("SUPERMODE!!! Time left: {0}", hero.GetSuperCounter());
                }
                else
                {
                    Console.SetCursorPosition(65, 4);
                    Console.Write("                            ");
                    blinky.MoveToTarget(f);
                    blinky.WriteOnField(f);
                    pinky.MoveToTarget(f);
                    pinky.WriteOnField(f);
                    inky.MoveToTarget(f);
                    inky.WriteOnField(f);
                    clyde.MoveToTarget(f);
                    clyde.WriteOnField(f);
                }
                if (((blinky.IsCatched(hero)) || (pinky.IsCatched(hero)) || (inky.IsCatched(hero)) || (clyde.IsCatched(hero))) && (!hero.SuperMode()))
                {
                    blinky.SetTargetToCorner();
                    pinky.SetTargetToCorner();
                    inky.SetTargetToCorner();
                    clyde.SetTargetToCorner();
                    counter = 0;
                    hero.WasCatched();
                }
                if (hero.GetLives()==0)
                {
                    break;
                }
                hero.WriteOnField(f);
                f.WriteField(0,0);
                Console.SetCursorPosition(73, 1);
                Console.WriteLine("{0}", hero.GetScore());
                Console.SetCursorPosition(73, 2);
                Console.WriteLine("{0}", hero.GetLives());
                Console.SetCursorPosition(72, 3);
                Console.WriteLine("{0}", time);
                if (!f.ArePoints())
                {
                    break;
                }
                Console.SetCursorPosition(0, 0);
                Thread.Sleep(levelspeed);
                f.ClearField();
                this.time++;
            }
            this.time = hero.GetScore();
        }

        public int GetRecord()
        {
            return this.time;
        }
    }
}
