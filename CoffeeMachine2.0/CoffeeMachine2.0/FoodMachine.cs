using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace CoffeeMachine2._0
{
    class FoodMachine:Machine
    {
        public FoodMachine(Bank b, Cooker c)
        {
            this.bank = b;
            this.cooker = c;
            
        }

        public override void Start()
        {
            cooker.Show();
            while (true)
            {
                bank.EnterMoney();
                int x = Choose();
                if (x > 0)
                {
                    cooker.Make(x);
                }
                else
                {
                    Console.WriteLine("Not enough money!");
                }
            }
        }

        public override int Choose()
        {
            Console.WriteLine("Choose your lunch:");
            int x = Convert.ToInt32(Console.ReadLine());
            if (bank.Money >= cooker.recipes[x - 1].Cost)
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

    class FoodCooker : Cooker
    {
        public FoodCooker(Recipes book, Components c)
        {
            this.book = book;
            this.component = c;
            this.recipes = new List<Recipe>();
        }

        public override void Show()
        {
            for (int i = 0; i < recipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {recipes[i]}");
            }
        }

        public override void GetRecipes()
        {
            book.Check(component);
            this.recipes = book.possible;
        }

        public override void Make(int k)
        {
            component.Give(recipes[k - 1]);
            Random r = new Random();
            foreach (string s in recipes[k - 1].Component)
            {
                Console.WriteLine($"Giving {s}...");
                Thread.Sleep(r.Next(500, 1500));
            }
            Console.WriteLine($"There is your {recipes[k - 1].Name}!");
        }
    }

    class FoodBook : Recipes
    {
        public FoodBook()
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

    class FoodRecipe : Recipe
    {
        public FoodRecipe(int x, string s)
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

    class FoodComponent : Components
    {
        public FoodComponent()
        {
            this.Count = new List<int>();
            this.Names = new List<string>();
        }

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
    }
}
