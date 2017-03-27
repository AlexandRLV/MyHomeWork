using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine
{
    class Machine
    {
        public ComponentsBook components { get; }
        public SugarPacket sugar { get; }
        public Interfaces face { get; }
        public WaterResourses water { get; }
        public RecipeBook recipe { get; }
        public Glasses glass { get; }
        public Banks bank { get; }
        public Card card { get; }

        public Machine(RecipeBook r, Banks b, SugarPacket s, WaterResourses w, Glasses g, Interfaces i, ComponentsBook c, Card a)
        {
            recipe = r;
            bank = b;
            sugar = s;
            water = w;
            glass = g;
            face = i;
            components = c;
            card = a;
        }

        public bool Make(int x, int s, int w)
        {
            if (x > 0)
            {
                if (!glass.HaveGlasses())
                {
                    Console.WriteLine("There is no glasses.");
                    return false;
                }
                else
                {
                    Console.WriteLine("Giving glass...");
                    glass.GiveGlass();
                    System.Threading.Thread.Sleep(700);
                }
                Console.WriteLine("Processing...");
                System.Threading.Thread.Sleep(1000);
                if (s > 0)
                {
                    Console.WriteLine("Adding sugar...");
                    System.Threading.Thread.Sleep(700);
                }
                if (w > 0)
                {
                    Console.WriteLine("Adding water...");
                    System.Threading.Thread.Sleep(700);
                }
                Console.WriteLine("There is your coffee!");
                Console.WriteLine($"There is your cash: {bank.Show()}");
                return true;
            }
            else
            {
                Console.WriteLine("Not enough money!");
                return false;
            }
        }

        public void Start()
        {
            face.Show(this);
            while (true)
            {
                int x = face.EnterMoney(this);
                bank.AddMoney(x);
                x = face.Choose(this);
                int s;
                if (sugar.HowManySugar() > 0)
                {
                    s = face.ChooseSugar(this);
                }
                else
                {
                    Console.WriteLine("We have no sugar.");
                    s = 0;
                }
                int w;
                if (water.HaveEnoughWater(10))
                {
                    w = 10;
                    water.GiveWater(10);
                }
                else
                {
                    if (water.HowManyWater() > 0)
                    {
                        w = water.HowManyWater();
                        water.GiveWater(w);
                    }
                    else
                    {
                        Console.WriteLine("We have no water.");
                        w = 0;
                    }
                }
                if (Make(x, s, w))
                {
                    break;
                }
            }
        }
    }
}
