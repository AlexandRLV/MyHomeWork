using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine2._0
{
    class Program
    {
        static void StartCoffeeMachine()
        {
            Card c = new MyCard(100, "12345");
            Bank b = new MyBank(c);
            Recipes r = new CoffeeBook();
            r.ReadRecipes("Recipes.txt");
            Components comp = new CoffeeComponent();
            comp.ReadComponents("Components.txt");
            Cooker cook = new CoffeeCooker(r, comp);
            cook.GetRecipes();
            Machine m = new CoffeeMachine(b, cook, 20);
            m.Start();
        }

        static void StartFoodMachine()
        {
            Card c = new MyCard(100, "12345");
            Bank b = new MyBank(c);
            Recipes r = new FoodBook();
            r.ReadRecipes("FoodRecipe.txt");
            Components comp = new FoodComponent();
            comp.ReadComponents("FoodComponent.txt");
            Cooker cook = new FoodCooker(r, comp);
            cook.GetRecipes();
            Machine m = new FoodMachine(b, cook);
            m.Start();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Choose machine: \r\n1. CoffeeMachine\r\n2. FoodMachine");
            int x = int.Parse(Console.ReadLine());
            if (x == 1)
            {
                StartCoffeeMachine();
            }
            else
            {
                StartFoodMachine();
            }
        }
    }
}
