using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            ComponentsBook c = new ComponentsBook();
            c.ReadComponents("Components.txt");
            RecipeBook r = new RecipeBook();
            r.ReadRecipes("Recipes.txt");
            r.CheckComponents(c);
            Banks b = new Banks();
            SugarPacket s = new SugarPacket(100);
            WaterResourses w = new WaterResourses(100);
            Glasses g = new Glasses(100);
            Interfaces i = new Interfaces();
            Card a = new Card("1234", 1000);
            Machine m = new Machine(r, b, s, w, g, i, c, a);
            m.Start();
        }
    }
}
