using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynthaxAnalyzer
{
    class Tokenizer
    {
        private bool TestNumber(char c)
        {
            int n;
            return int.TryParse(c.ToString(), out n);
        }

        private bool TestChar(char c)
        {
            string abs = "abcdefghigklmnopqrstuvwxyzABCDEFGHIGKLMNOPQRSTUVWXYZ";
            return abs.Contains(c);
        }

        private bool TestOper(char c)
        {
            string oper = "&|+-*";
            return oper.Contains(c);
        }

        public List<MyToken> GetTokenz(string Input)
        {
            List<MyToken> Tokenz = new List<MyToken>();
            int current = 0;
            while (current< Input.Length)
            {
                char c = Input[current];
                if (c == '(')
                {
                    Tokenz.Add(new MyToken("paren", "("));
                    current++;
                    continue;
                }
                if (c == ')')
                {
                    Tokenz.Add(new MyToken("paren", ")"));
                    current++;
                    continue;
                }
                if (c==' ')
                {
                    current++;
                    continue;
                }
                if (TestNumber(c))
                {
                    string value = "";
                    while (TestNumber(c))
                    {
                        value += c;
                        c = Input[++current];
                    }
                    Tokenz.Add(new MyToken("number", value));
                    continue;
                }
                if (TestChar(c))
                {
                    string value = "";
                    while (TestChar(c)||TestNumber(c))
                    {
                        value += c;
                        c = Input[++current];
                    }
                    Tokenz.Add(new MyToken("name", value));
                    continue;
                }
                if (TestOper(c))
                {
                    string value = "";
                    while (TestOper(c))
                    {
                        value += c;
                        c = Input[++current];
                    }
                    Tokenz.Add(new MyToken("oper", value));
                    continue;
                }
                if (c == ':')
                {
                    c = Input[++current];
                    if (c == '=')
                    {
                        current++;
                        Tokenz.Add(new MyToken("equal", ":="));
                        continue;
                    }
                }
                if (c == ';')
                {
                    break;
                }
                throw new Exception("Unknown character: " + c);
            }
            return Tokenz;
        }
    }

    class MyToken
    {
        public string Type;
        public string Value;

        public MyToken(string type, string value)
        {
            this.Type = type;
            this.Value = value;
        }

        public override string ToString()
        {
            return string.Format($"{Type} - {Value}");
        }
    }

    class TokenNode
    {
        public MyToken Info;
        //public List<TokenNode> tokens;
        public TokenNode Left;
        public TokenNode Right;
    }

    class TokenTree
    {
        public TokenNode Root;

        public TokenTree()
        {
            this.Root = new TokenNode();
        }

        public void PrintLevels()
        {
            Queue<TokenNode> q1 = new Queue<TokenNode>();
            Queue<TokenNode> q2 = new Queue<TokenNode>();

            q1.Enqueue(Root);
            while (q1.Count > 0)
            {
                PrintQueue(q1);
                while (q1.Count > 0)
                {
                    TokenNode t = q1.Dequeue();
                    if (t.Left != null)
                    {
                        q2.Enqueue(t.Left);
                    }
                    if (t.Right != null)
                    {
                        q2.Enqueue(t.Right);
                    }
                }
                q1 = new Queue<TokenNode>(q2);
                q2.Clear();
            }
        }

        private void PrintQueue(Queue<TokenNode> q)
        {
            Queue<TokenNode> q1 = new Queue<TokenNode>(q);
            while (q1.Count > 0)
            {
                Console.Write($"{q1.Dequeue().Info} ");
            }
            Console.WriteLine();
        }
    }

    class Parser
    {
        public TokenTree Parse(List<MyToken> tokens)
        {
            int current = 0;
            TokenTree t = new TokenTree();
            t.Root.Info = tokens.Find(a => a.Value == ":=");
            t.Root.Left = Dis(tokens, ref current);
            current++;
            t.Root.Right = Dis(tokens, ref current);
            return t;
        }

        private TokenNode Dis(List<MyToken> tokens, ref int current)
        {
            TokenNode t = Imp(tokens, ref current);
            if (current < tokens.Count)
            {
                if (tokens[current].Value == "|")
                {
                    TokenNode t1 = new TokenNode();
                    t1.Info = tokens[current];
                    t1.Left = t;
                    current++;
                    t1.Right = Dis(tokens, ref current);
                    return t1;
                }
            }
            return t;
        }

        private TokenNode Imp(List<MyToken> tokens, ref int current)
        {
            TokenNode t = Plus(tokens, ref current);
            if (current < tokens.Count)
            {
                if (tokens[current].Value == "-")
                {
                    TokenNode t1 = new TokenNode();
                    t1.Info = tokens[current];
                    t1.Left = t;
                    current++;
                    t1.Right = Imp(tokens, ref current);
                    return t1;
                }
            }
            return t;
        }

        private TokenNode Plus(List<MyToken> tokens, ref int current)
        {
            TokenNode t = Shef(tokens, ref current);
            if (current < tokens.Count)
            {
                if (tokens[current].Value == "+")
                {
                    TokenNode t1 = new TokenNode();
                    t1.Info = tokens[current];
                    t1.Left = t;
                    current++;
                    t1.Right = Shef(tokens, ref current);
                    return t1;
                }
            }                
            return t;
        }

        private TokenNode Shef(List<MyToken> tokens, ref int current)
        {
            TokenNode t = Kon(tokens, ref current);
            if (current < tokens.Count)
            {
                if (tokens[current].Value == "*")
                {
                    TokenNode t1 = new TokenNode();
                    t1.Info = tokens[current];
                    t1.Left = t;
                    current++;
                    t1.Right = Shef(tokens, ref current);
                    return t1;
                }
            }
            return t;
        }

        private TokenNode Kon(List<MyToken> tokens, ref int current)
        {
            TokenNode t = new TokenNode();
            t.Info = tokens[current];
            current++;
            if (current < tokens.Count)
            {
                if (tokens[current].Value == "&")
                {
                    TokenNode t1 = new TokenNode();
                    t1.Info = tokens[current];
                    t1.Left = t;
                    current++;
                    t1.Right = Kon(tokens, ref current);
                    return t1;
                }
            }
            return t;
        }
    }

    class Variable
    {
        public string Info;
        public string Name;

        public Variable(string x, string s)
        {
            this.Info = x;
            this.Name = s;
        }

        public override string ToString()
        {
            return String.Format($"{Name}={Info}");
        }
    }

    class Analyzer
    {
        public List<MyToken> Tokenz;
        public Parser parser;
        public TokenTree tree;
        public string Input;
        public Tokenizer tokenizer;
        public List<Variable> variables;

        public Analyzer()
        {
            this.tokenizer = new Tokenizer();
            this.tree = new TokenTree();
            this.parser = new Parser();
            this.variables = new List<Variable>();
        }

        public void WriteTokenz()
        {
            foreach (MyToken t in Tokenz)
            {
                Console.WriteLine(t);
            }
        }

        private void GetTokenz()
        {
            this.Tokenz = this.tokenizer.GetTokenz(Input);
        }

        private void Parse()
        {
            this.tree = this.parser.Parse(Tokenz);
        }

        private string Dis(string x, string y)
        {
            string x1 = x;
            string y1 = y;
            string res = "";
            for (int i = 0; i < 8; i++)
            {
                if ((x1[i] == '1') || (y1[i] == '1'))
                {
                    res += '1';
                }
                else res += '0';
            }
            return res;
        }

        private string Shef(string x, string y)
        {
            string x1 = x;
            string y1 = y;
            string res = "";
            for (int i = 0; i < 8; i++)
            {
                if ((x1[i] == '0') || (y1[i] == '0'))
                {
                    res += '1';
                }
                else res += '0';
            }
            return res;
        }

        private string Imp(string x, string y)
        {
            string x1 = x;
            string y1 = y;
            string res = "";
            for (int i = 0; i < 8; i++)
            {
                if ((x1[i] == '1') && (y1[i] == '0'))
                {
                    res += '0';
                }
                else res += '1';
            }
            return res;
        }

        private string Kon(string x, string y)
        {
            string x1 = x;
            string y1 = y;
            string res = "";
            for (int i = 0; i < 8; i++)
            {
                if ((x1[i] == '1') && (y1[i] == '1'))
                {
                    res += '1';
                }
                else res += '0';
            }
            return res;
        }

        private string Plus(string x, string y)
        {
            string x1 = x.ToString();
            string y1 = y.ToString();
            string res = "";
            for (int i = 0; i < 8; i++)
            {
                if (x1[i] != y1[i])
                {
                    res += '1';
                }
                else res += '0';
            }
            return res;
        }

        public void Calculate(string s)
        {
            this.Input = s;
            GetTokenz();
            Parse();
            if (variables.Where(a => a.Name == this.tree.Root.Left.Info.Value).Count() == 0)
            {
                variables.Add(new Variable(Calculate(this.tree.Root.Right), this.tree.Root.Left.Info.Value));
            }
            else
            {
                variables.Where(a => a.Name == this.tree.Root.Left.Info.Value).ElementAt(0).Info = Calculate(this.tree.Root.Right);
            }
        }

        private string Calculate(TokenNode t)
        {
            if (t.Left == t.Right)
            {
                if (t.Info.Type == "name")
                {
                    return this.variables.Find(a => a.Name == t.Info.Value).Info;
                }
                if (t.Info.Type == "number")
                {
                    return t.Info.Value;
                }
            }
            else
            {
                if (t.Info.Value == "|")
                {
                    return Dis(Calculate(t.Left), Calculate(t.Right));
                }
                if (t.Info.Value == "&")
                {
                    return Kon(Calculate(t.Left), Calculate(t.Right));
                }
                if (t.Info.Value == "+")
                {
                    return Plus(Calculate(t.Left), Calculate(t.Right));
                }
                if (t.Info.Value == "*")
                {
                    return Shef(Calculate(t.Left), Calculate(t.Right));
                }
                if (t.Info.Value == "-")
                {
                    return Imp(Calculate(t.Left), Calculate(t.Right));
                }
            }
            throw new Exception("Wrong syntax!");
        }

        public void WriteVariables()
        {
            Console.WriteLine("Variables:");
            foreach (Variable v in variables)
            {
                Console.WriteLine(v);
            }
        }
    }
}
