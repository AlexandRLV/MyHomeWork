using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CoffeeMachine
{
    class ComponentsBook
    {
        private Dictionary<string, int> components;

        public ComponentsBook()
        {
            components = new Dictionary<string, int>();
        }

        public void ReadComponents(string path)
        {
            using (StreamReader file = new StreamReader(path))
            {
                while (!file.EndOfStream)
                {
                    string[] s = file.ReadLine().Split(':');
                    components.Add(s[0], int.Parse(s[1]));
                }
            }
        }

        public bool HasComponent(string s, int n)
        {
            if (components.ContainsKey(s))
                if (components[s] >= n)
                    return true;
            return false;
        }

        public void GiveComponent(string s, int n)
        {
            if (components.ContainsKey(s))
            {
                if (components[s] >= n)
                {
                    components[s] -= n;
                }
            }
        }
    }

    class SugarPacket
    {
        private int sugar;

        public SugarPacket(int s)
        {
            sugar = s;
        }

        public void GiveSugar(int s)
        {
            sugar -= s;
        }

        public bool HaveEnoughSugar(int s)
        {
            return sugar >= s;
        }

        public int HowManySugar()
        {
            return sugar;
        }
    }

    class Interfaces
    {
        public void Show(Machine m)
        {
            Console.WriteLine("Recipes:");
            for (int i = 0; i < m.recipe.recipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {m.recipe.recipes[i]}");
            }
        }

        public void AddMoney(int x, Machine m)
        {
            m.bank.AddMoney(x);
        }

        public int Choose(Machine m)
        {
            Console.WriteLine("Choose coffee:");
            int x = Convert.ToInt32(Console.ReadLine());
            if (m.bank.HaveEnoughMoney(m.recipe.Cost(x - 1)))
            {
                m.bank.GiveMoney(m.recipe.Cost(x - 1));
                return x;
            }
            else
            {
                return -1;
            }
        }

        public int EnterMoney(Machine m)
        {
            Console.WriteLine($"Your money: {m.bank.Show()}");
            while (true)
            {
                Console.WriteLine("Would you pay by cash or card?");
                Console.WriteLine("1. Cash \r\n2. Card");
                int x = int.Parse(Console.ReadLine());
                if (x == 1)
                {
                    Console.WriteLine("Enter money:");
                    x = int.Parse(Console.ReadLine());
                    return x;
                }
                else if (x == 2)
                {
                    while (true)
                    {
                        Console.WriteLine("Enter PIN:");
                        string s = Console.ReadLine();
                        if (m.card.CheckPin(s))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Wrong PIN!");
                        }
                    }
                    Console.WriteLine("Enter money:");
                    x = int.Parse(Console.ReadLine());
                    if (m.card.HaveEnougnMoney(x))
                    {
                        m.card.GiveMoney(x);
                        return x;
                    }
                    else
                    {
                        Console.WriteLine("Not enough money!");
                    }
                }
            }
        }

        public int ChooseSugar(Machine m)
        {
            Console.WriteLine("How many sugar?");
            int x = Convert.ToInt32(Console.ReadLine());
            if (m.sugar.HaveEnoughSugar(x))
            {
                m.sugar.GiveSugar(x);
                return x;
            }
            else
            {
                x = m.sugar.HowManySugar();
                Console.WriteLine($"Not so many sugar, giving: {x}");
                m.sugar.GiveSugar(x);
                return x;
            }
        }
    }

    class WaterResourses
    {
        private int water;

        public WaterResourses(int x)
        {
            water = x;
        }

        public void GiveWater(int x)
        {
            water -= x;
        }

        public bool HaveEnoughWater(int x)
        {
            return water >= x;
        }

        public int HowManyWater()
        {
            return water;
        }
    }

    class RecipeBook
    {
        public List<Recipe> recipes { get; }
        public Dictionary<string, Dictionary<string, int>> components { get; }

        public void CheckComponents(ComponentsBook c)
        {
            List<Recipe> NONpossible = new List<Recipe>();
            foreach (Recipe r in recipes)
            {
                for (int i = 0; i < components[r.name].Count; i++)
                {
                    string s = components[r.name].ElementAt(i).Key;
                    int n = components[r.name].ElementAt(i).Value;
                    if (!c.HasComponent(s, n))
                    {
                        NONpossible.Add(r);
                    }
                }
            }
            foreach (Recipe r in NONpossible)
            {
                recipes.Remove(r);
            }
        }

        public void Make(int n, ComponentsBook c)
        {
            for (int i = 0; i < components[recipes[n].name].Count; i++)
            {
                string s = components[recipes[n].name].ElementAt(i).Key;
                int a = components[recipes[n].name].ElementAt(i).Value;
                c.GiveComponent(s, a);
            }
        }

        public RecipeBook()
        {
            recipes = new List<Recipe>();
            components = new Dictionary<string, Dictionary<string, int>>();
            //recipes.Add(new Recipe("Standart", 15));
            //ReadRecipes(path);
        }

        public void Add(Recipe r)
        {
            recipes.Add(r);
        }

        public int Cost(int x)
        {
            return recipes[x].cost;
        }

        public void ReadRecipes(string path)
        {
            using (StreamReader file = new StreamReader(path))
            {
                while (!file.EndOfStream)
                {
                    string[] s = file.ReadLine().Split(':');
                    recipes.Add(new Recipe(s[0], int.Parse(s[1])));
                    components.Add(s[0], new Dictionary<string, int>());
                    for (int i = 2; i < s.Length; i++)
                    {
                        string[] a = s[i].Split(' ');
                        components[s[0]].Add(a[0], int.Parse(a[1]));
                    }
                }
            }
        }
    }

    class Recipe
    {
        public string name;
        public int cost;
        public Recipe(string n, int c)
        {
            name = n;
            cost = c;
        }

        public override string ToString()
        {
            return $"{name} {cost}";
        }
    }

    class Glasses
    {
        private int glasses;

        public Glasses(int n)
        {
            glasses = n;
        }

        public bool HaveGlasses()
        {
            return glasses > 0;
        }

        public void GiveGlass()
        {
            glasses--;
        }
    }

    class Banks
    {
        private int money = 0;

        public void AddMoney(int c)
        {
            money += c;
        }

        public bool HaveEnoughMoney(int c)
        {
            if (money >= c)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void GiveMoney(int c)
        {
            money -= c;
        }

        public int Show()
        {
            return money;
        }
    }

    class Card
    {
        private string Pin;
        private int Money;

        public Card(string p, int m)
        {
            Pin = p;
            Money = m;
        }

        public bool CheckPin(string s)
        {
            return Pin.Equals(s);
        }

        public void GiveMoney(int m)
        {
            Money -= m;
        }

        public bool HaveEnougnMoney(int m)
        {
            return Money >= m;
        }
    }
}
