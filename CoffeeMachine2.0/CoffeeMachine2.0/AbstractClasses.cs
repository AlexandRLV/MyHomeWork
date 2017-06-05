using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine2._0
{
    public abstract class Machine
    {
        protected Bank bank;
        protected Cooker cooker;
        abstract public void Start();
        abstract public int Choose();
    }
    
    public abstract class Bank
    {
        public int Money { get; set; }
        public Card card;
        abstract public void EnterMoney();
        abstract public int EnterByCash();
        abstract public int EnterByCard();
    }

    public abstract class Card
    {
        protected string Pin;
        public int Money { get; set; }
        abstract public bool Check(string s);
    }

    public abstract class Cooker
    {
        protected Recipes book;
        protected Components component;
        public List<Recipe> recipes;
        abstract public void Show();
        abstract public void Make(int k);
        abstract public void GetRecipes();
    }

    public abstract class Recipes
    {
        public List<Recipe> known;
        public List<Recipe> possible;
        abstract public void Check(Components c);
        abstract public void ReadRecipes(string path);
    }

    public abstract class Recipe
    {
        public string Name;
        public int Cost;
        public List<string> Component;
        public List<int> Count;
        abstract public void AddComponent(string s, int x);
    }

    public abstract class Components
    {
        public List<string> Names;
        public List<int> Count;
        abstract public void ReadComponents(string path);
        abstract public bool CheckRecipe(Recipe r);
        abstract public void Give(Recipe r);
    }
}
