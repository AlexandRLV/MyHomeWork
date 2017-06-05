using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoffeeMachine2._0;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Components c = new CoffeeComponent();
            c.ReadComponents("Components.txt");
            Recipe r = new CoffeeRecipe("A", 20);
            r.AddComponent("Water", 10000);
            Recipe r2 = new CoffeeRecipe("A", 20);
            r2.AddComponent("Water", 10);
            r2.AddComponent("Coffee", 10);
            bool b = c.CheckRecipe(r);
            bool b2 = c.CheckRecipe(r2);
            Assert.AreEqual(b, false);
            Assert.AreEqual(b2, true);
        }

        [TestMethod]
        public void TestMethod2()
        {
            Components c = new CoffeeComponent();
            c.ReadComponents("Components.txt");
            Assert.AreEqual(c.Count.Count, c.Names.Count);
        }

        [TestMethod]
        public void TestMethod3()
        {
            Recipes r = new CoffeeBook();
            r.ReadRecipes("Recipes.txt");
            foreach (Recipe a in r.known)
            {
                Assert.AreEqual(a.Component.Count, a.Count.Count);
            }
        }
    }
}
