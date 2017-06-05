using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace CoffeeMachine2._0
{
    class CoffeeMachine:Machine
    {
        private int Glasses;
        public CoffeeMachine(Bank b, Cooker c, int g)
        {
            this.bank = b;
            this.cooker = c;
            this.Glasses = g;         
        }

        public override void Start()
        {
            cooker.Show();
            while (true)
            {
                if (Glasses > 0)
                {
                    bank.EnterMoney();
                    int x = Choose();
                    if (x > 0)
                    {
                        cooker.Make(x);
                        Glasses--;
                    }
                    else
                    {
                        Console.WriteLine("Not enough money!");
                    }
                }
                else
                {
                    Console.WriteLine("We have no glasses!");
                }
            }
        }

        public override int Choose()
        {
            Console.WriteLine("Choose coffee:");
            int x = Convert.ToInt32(Console.ReadLine());
            if (bank.Money>=cooker.recipes[x-1].Cost)
            {
                bank.Money -= cooker.recipes[x - 1].Cost;
                return x;
            }
            else
            {
                return -1;
            }
        }
    }

    class MyBank:Bank
    {
        public override void EnterMoney()
        {
            Console.WriteLine($"Your money: {Money}");
            while (true)
            {
                Console.WriteLine("Would you pay by cash or card?");
                Console.WriteLine("1. Cash \r\n2. Card");
                int x = int.Parse(Console.ReadLine());
                if (x == 1)
                {
                    this.Money += EnterByCash();
                    return;
                }
                else if (x == 2)
                {
                    this.Money += EnterByCard();
                    return;
                }
            }
        }

        public override int EnterByCash()
        {
            Console.WriteLine("Enter money:");
            int x = int.Parse(Console.ReadLine());
            return x;
        }

        public override int EnterByCard()
        {
            while (true)
            {
                Console.WriteLine("Enter PIN:");
                string s = Console.ReadLine();
                if (card.Check(s))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Wrong PIN!");
                }
            }
            Console.WriteLine("Enter money:");
            int x = int.Parse(Console.ReadLine());
            if (card.Money>=x)
            {
                card.Money -= x;
                return x;
            }
            else
            {
                Console.WriteLine("Not enough money!");
                return 0;
            }
        }

        public MyBank(Card c)
        {
            card = c;
            Money = 0;
        }
    }

    public class MyCard : Card
    {
        public MyCard(int x, string Pin)
        {
            this.Money = x;
            this.Pin = Pin;
        }

        public override bool Check(string s)
        {
            return this.Pin == s;
        }
    }

    public class CoffeeCooker : Cooker
    {
        public CoffeeCooker(Recipes book, Components c)
        {
            this.book = book;
            this.component = c;
            this.recipes = new List<Recipe>();
        }

        public override void GetRecipes()
        {
            book.Check(component);
            this.recipes = book.possible;
        }

        public override void Show()
        {
            for (int i = 0; i < recipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {recipes[i]}");
            }
        }

        public override void Make(int k)
        {
            component.Give(recipes[k - 1]);
            Random r = new Random();
            Console.WriteLine("Adding glass...");
            Thread.Sleep(300);
            foreach (string s in recipes[k - 1].Component)
            {
                Console.WriteLine($"Adding {s}...");
                Thread.Sleep(r.Next(500, 1500));
            }
            Console.WriteLine($"There is your {recipes[k - 1].Name}!");
        }
    }

    public class CoffeeBook :Recipes
    {
        public CoffeeBook()
        {
            this.known = new List<Recipe>();
            this.possible = new List<Recipe>();
        }

        public override void Check(Components c)
        {
            possible.Clear();
            foreach (CoffeeRecipe r in known)
            {
                if (c.CheckRecipe(r))
                {
                    possible.Add(r);
                }
            }
        }

        public override void ReadRecipes(string path)
        {
            using (StreamReader file = new StreamReader(path))
            {
                while (!file.EndOfStream)
                {
                    string[] s = file.ReadLine().Split(':');
                    CoffeeRecipe r = new CoffeeRecipe(s[0], int.Parse(s[1]));
                    for (int i = 2; i < s.Length; i++)
                    {
                        string[] a = s[i].Split(' ');
                        r.AddComponent(a[0], int.Parse(a[1]));
                    }
                    this.known.Add(r);
                }
            }
        }
    }

    public class CoffeeRecipe : Recipe
    {
        public CoffeeRecipe(string s, int x)
        {
            this.Name = s;
            this.Cost = x;
            this.Component = new List<string>();
            this.Count = new List<int>();
        }

        public override void AddComponent(string s, int x)
        {
            this.Component.Add(s);
            this.Count.Add(x);
        }

        public override string ToString()
        {
            return String.Format($"{this.Name} - {this.Cost}");
        }
    }

    public class CoffeeComponent : Components
    {
        public override void ReadComponents(string path)
        {
            using (StreamReader file = new StreamReader(path))
            {
                while (!file.EndOfStream)
                {
                    string[] s = file.ReadLine().Split(':');
                    Names.Add(s[0]);
                    Count.Add(int.Parse(s[1]));
                }
            }
        }

        public override bool CheckRecipe(Recipe r)
        {
            foreach (string s in r.Component)
            {
                if ((!this.Names.Contains(s)) || (this.Count[this.Names.IndexOf(s)] < r.Count[r.Component.IndexOf(s)]))
                {
                    return false;
                }
            }
            return true;
        }

        public override void Give(Recipe r)
        {
            foreach (string s in r.Component)
            {
                this.Count[this.Names.IndexOf(s)] -= r.Count[r.Component.IndexOf(s)];
            }
        }

        public CoffeeComponent()
        {
            this.Names = new List<string>();
            this.Count = new List<int>();
        }
    }
}
