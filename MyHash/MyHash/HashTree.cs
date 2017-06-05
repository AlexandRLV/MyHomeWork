using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHash
{
    class HashTree
    {
        public class TreeNode
        {
            public string Info { get; set; }
            public int Level { get; set; }
            public TreeNode Left { get; set; }
            public TreeNode Right { get; set; }
        }

        public TreeNode Root { get; private set; }

        public void PrintLevels()
        {
            Queue<TreeNode> q1 = new Queue<TreeNode>();
            Queue<TreeNode> q2 = new Queue<TreeNode>();

            q1.Enqueue(Root);
            while (q1.Count > 0)
            {
                PrintQueue(q1);
                while (q1.Count > 0)
                {
                    TreeNode t = q1.Dequeue();
                    if (t.Left != null)
                    {
                        q2.Enqueue(t.Left);
                    }
                    if (t.Right != null)
                    {
                        q2.Enqueue(t.Right);
                    }
                }
                q1 = new Queue<TreeNode>(q2);
                q2.Clear();
            }
        }

        private void PrintQueue(Queue<TreeNode> q)
        {
            Queue<TreeNode> q1 = new Queue<TreeNode>(q);
            while (q1.Count > 0)
            {
                Console.Write($"{q1.Dequeue().Info}  ");
            }
            Console.WriteLine();
        }

        public HashTree(int[] x)
        {
            Root = Create(x);
        }

        private TreeNode Create(IEnumerable<int> x)
        {
            TreeNode t = new TreeNode();
            if (x.Count() == 1)
            {
                t.Info = LeafHash(x.ElementAt(0));
                t.Level = 0;
            }
            else
            {
                t.Left = Create(x.Take(x.Count() / 2));
                t.Right = Create(x.Skip(x.Count() / 2));
                t.Info = InternalHash(t.Left.Info, t.Right.Info);
                t.Level = t.Right.Level + 1;
            }
            return t;
        }

        private string Hash(string input)// Хеш-функция RS
        {
            uint hash = 0;
            uint a = 63689;
            uint b = 378551;
            for (int i = 0; i < input.Length; i++)
            {
                hash = hash * a + (byte)input[i];
                a *= b;
            }

            return hash.ToString();
        }

        private string LeafHash(int input)  // Нахождение хеша для листьев
        {
            return "00h" + Hash(input.ToString());
        }

        private string InternalHash(string input1, string input2) // Нахождение хеша для узлов
        {
            return "01h" + Hash(input1) + Hash(input2);
        }
    }
}
