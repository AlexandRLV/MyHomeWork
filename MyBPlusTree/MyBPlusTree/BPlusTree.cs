using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBPlusTree
{
    class BPlusTree<T> where T:IComparable
    {
        // Элемент Ключ - Значение
        public class Keys<T>
        {
            public int Key;
            public T Info;

            public Keys(int key, T info)
            {
                this.Key = key;
                this.Info = info;
            }

            public Keys()
            {

            }

            public Keys(Keys<T> key)
            {
                this.Key = key.Key;
                this.Info = key.Info;
            }

            public void Copy(Keys<T> key)
            {
                this.Key = key.Key;
                this.Info = key.Info;
            }

            public override string ToString()
            {
                return String.Format($"{Key}");
            }
        }

        // Элемент дерева, содержит списки ключей и потомков.
        public class Node<T> where T:IComparable
        {
            public bool IsLeaf;
            public List<Node<T>> Next;
            public List<Keys<T>> Keys;
            public Node<T> Neighbour;

            public Node()
            {
                Next = new List<Node<T>>();
                Keys = new List<Keys<T>>();
            }

            public Node(Node<T> node)
            {
                this.IsLeaf = node.IsLeaf;
                this.Neighbour = node.Neighbour;
                Next = new List<Node<T>>();
                Keys = new List<Keys<T>>();
                if (node.Keys.Count != 0)
                {
                    foreach (var t in node.Keys)
                    {
                        this.Keys.Add(new Keys<T>(t));
                    }
                    foreach (var t in node.Next)
                    {
                        this.Next.Add(new Node<T>(t));
                    }
                }
            }
        }

        // Степень дерева - максимальное число ключей в одном узле/листе.
        public int Degree;
        public Node<T> Root;

        public BPlusTree(int degree)
        {
            Degree = degree;
            Root = new Node<T>();
            Root.IsLeaf = true;
        }

        // Вывод дерева по уровням.
        public void PrintLevels()
        {
            Queue<Node<T>> q1 = new Queue<Node<T>>();
            Queue<Node<T>> q2 = new Queue<Node<T>>();

            q1.Enqueue(Root);
            while (q1.Count > 0)
            {
                PrintQueue(q1);
                while (q1.Count > 0)
                {
                    Node<T> t = q1.Dequeue();
                    foreach(var b in t.Next)
                    {
                        q2.Enqueue(b);
                    }
                }
                q1 = new Queue<Node<T>>(q2);
                q2.Clear();
            }
        }

        private void PrintQueue(Queue<Node<T>> q)
        {
            Queue<Node<T>> q1 = new Queue<Node<T>>(q);
            while (q1.Count > 0)
            {
                var b = q1.Dequeue();
                foreach (var a in b.Keys)
                {
                    Console.Write($"{a.Key}  ");
                }
                Console.Write("| ");
                //Console.Write($"{q1.Dequeue()} ");
            }
            Console.WriteLine();
        }

        // Метод поиска значения по ключу.
        public T Find(int key)
        {
            return FindKey(key, Root);
        }

        private T FindKey(int key, Node<T> node)
        {
            // Если это - лист, возвращаем значение по ключу.
            if (node.IsLeaf)
            {
                return node.Keys.Find(a => a.Key == key).Info;
            }
            // Иначе - ищем, в какой из потомков перейти.
            else
            {
                // Перебираем ключи в узле.
                int i = 0;
                while (i < node.Keys.Count)
                {
                    int k = node.Keys[i].Key;
                    // Если нашли первый ключ, который больше искомого, идём в потомка с таким же номером.
                    if (key < k)
                    {
                        return FindKey(key, node.Next[i]);
                    }
                    // Если нашли ключ, равный искомому, идём в следующего, или последнего, если ключ последний, потомка.
                    else if (key == k)
                    {
                        if (i < node.Next.Count - 1)
                        {
                            i++;
                        }
                        return FindKey(key, node.Next[i]);
                    }
                    else
                    {
                        i++;
                    }
                }
                // Если дошли до конца, и не нашли равного или большего, идём в последнего потомка.
                return FindKey(key, node.Next[node.Next.Count-1]);
            }
        }

        // Метод добавления ключа.
        public void Add(Keys<T> key)
        {
            // Переменные для проверки, нужно ли разбиение, какой узел/лист добавился.
            bool needToSplit = false;
            Node<T> splitted = new Node<T>();
            AddKey(key, Root, ref needToSplit, ref splitted);
            // Если всё ещё нужно разбиение, создаём новый корень, увеличивая порядок дерева.
            if (needToSplit)
            {
                Node<T> newNode = new Node<T>();
                newNode.Keys = new List<Keys<T>>();
                newNode.Keys.Add(splitted.Keys[0]);
                newNode.Next = new List<Node<T>>();
                newNode.Next.Add(Root);
                newNode.Next.Add(splitted);
                Root = newNode;
            }
        }

        private void AddKey(Keys<T> key, Node<T> node, ref bool needToSplit, ref Node<T> splitted)
        {
            // Если мы в листе, добавляем ключ в его список.
            if (node.IsLeaf)
            {
                // Если число ключей меньше степени дерева, добавляем новый.
                if (node.Keys.Count < Degree)
                {
                    node.Keys.Add(key);
                }
                // Если больше или равно, разделяем лист на два, перемещая половину ключей в новый, добавляем новый ключ в новый лист.
                else
                {
                    splitted.IsLeaf = true;
                    splitted.Keys = node.Keys.Skip(node.Keys.Count / 2).ToList();
                    splitted.Keys.Add(key);
                    node.Keys = node.Keys.Take(node.Keys.Count / 2).ToList();
                    splitted.Neighbour = node.Neighbour;
                    node.Neighbour = splitted;
                    needToSplit = true;
                }
            }
            // Если мы не в листе, ищем, в какой из потомков перейти.
            else
            {
                int i = 0;
                while (i < node.Keys.Count)
                {
                    int k = node.Keys[i].Key;
                    if (key.Key < k)
                    {
                        AddKey(key, node.Next[i], ref needToSplit, ref splitted);
                        break;
                    }
                    else if (key.Key == k)
                    {
                        AddKey(key, node.Next[i+1], ref needToSplit, ref splitted);
                        break;
                    }
                    else
                    {
                        i++;
                    }
                }
                if (i == node.Keys.Count)
                {
                    AddKey(key, node.Next[node.Next.Count-1], ref needToSplit, ref splitted);
                }
                // Если после добавления ключа необходимо разбиение, значит, оно произошло уровнем ниже.
                if (needToSplit)
                {
                    // Если во время поиска дошли до конца списка, добавляем новый элемент в конец.
                    if ((i + 1 == node.Keys.Count)||(i == node.Keys.Count))
                    {
                        node.Next.Add(new Node<T>(splitted));
                        node.Keys.Add(new Keys<T>(splitted.Keys[0]));
                    }
                    // Иначе вставляем его на нужную позицию.
                    else
                    {
                        node.Next.Insert(i + 1, splitted);
                        node.Keys.Insert(i, splitted.Keys[0]);
                    }
                    // Если число ключей в текущем узле меньше степени, разбиение больше не нужно.
                    if (node.Keys.Count <= Degree)
                    {
                        needToSplit = false;
                    }
                    // Иначе, разбиваем текущий узел на два, перемещаем половину элементов в ноыый.
                    else
                    {
                        splitted.IsLeaf = false;
                        if (node.Keys.Count % 2 == 0)
                        {
                            splitted.Keys = node.Keys.Skip(node.Keys.Count / 2).ToList();
                            splitted.Next = node.Next.Skip(node.Next.Count / 2 + 1).ToList();
                            node.Keys = node.Keys.Take(node.Keys.Count / 2).ToList();
                            node.Next = node.Next.Take(node.Next.Count / 2 + 1).ToList();
                        }
                        else
                        {
                            splitted.Keys = node.Keys.Skip(node.Keys.Count / 2).ToList();
                            splitted.Next = node.Next.Skip(node.Next.Count / 2).ToList();
                            node.Keys = node.Keys.Take(node.Keys.Count / 2).ToList();
                            node.Next = node.Next.Take(node.Next.Count / 2).ToList();
                        }
                    }
                }
            }
        }

        // Метод удаления ключа.
        public void Delete(int key)
        {
            // Флаг для проверки необходимости объедиения узлов, элемент, передающий новый ключ, который нужно вставить заместо удалённого.
            bool needToMerge = false;
            Keys<T> returns = new Keys<T>();
            DeleteKey(key, Root, ref needToMerge, ref returns);
        }

        private void DeleteKey(int key, Node<T> node, ref bool needToMerge, ref Keys<T> returns)
        {
            // Если находимся в листе, удаляем ключ, если элемент остался пуст, передаём наверх, что нужно объединение.
            if (node.IsLeaf)
            {
                if (node.Keys.Where(a => a.Key == key).Count() != 0)
                {
                    if (node.Keys.Count == 1)
                    {
                        needToMerge = true;
                        node.Keys.Clear();
                    }
                    else
                    {
                        needToMerge = false;
                        node.Keys.Remove(node.Keys.Find(a=>a.Key==key));
                        returns.Copy(node.Keys[0]);
                    }
                }
            }
            // Если находимся в узле, ищем, в какой из потомков идти.
            else
            {
                int i = 0;
                while (i < node.Keys.Count)
                {
                    int k = node.Keys[i].Key;
                    if (key < k)
                    {
                        DeleteKey(key, node.Next[i], ref needToMerge, ref returns);
                        break;
                    }
                    else if ((key == k)||(i==node.Keys.Count-1))
                    {
                        if (i < node.Next.Count()-1)
                        {
                            i++;
                        }
                        DeleteKey(key, node.Next[i], ref needToMerge, ref returns);
                        break;
                    }
                    else
                    {
                        i++;
                    }
                }
                // Если необходимо объединение, значит уровнем ниже есть пустой узел/лист.
                if (needToMerge)
                {
                    // Если число потомков больше одного, удаляем пустой, иначе передаём наверх о необходимости объединения.
                    if (node.Next.Count > 1)
                    {
                        // Если в поиске остановились на последнем, удаляем последний.
                        if (i == node.Keys.Count)
                        {
                            node.Keys.RemoveAt(i - 1);
                            node.Next.RemoveAt(i);
                        }
                        // Если на первом, удаляем первый.
                        else if (i == 0)
                        {
                            node.Keys.RemoveAt(0);
                            node.Next.RemoveAt(0);
                        }
                        // Если в середине, то проверяем наличие в узле искокого ключа.
                        else
                        {
                            // Если он есть, удаляем.
                            if (node.Keys.Find(a => a.Key == key) != null)
                            {
                                node.Keys.Remove(node.Keys.Find(a => a.Key == key));
                            }
                            // Иначе удаляем ключ, ведущий на пустого потомка.
                            else
                            {
                                node.Keys.RemoveAt(i - 1);
                            }
                            // Удаляем пустого потомка.
                            node.Next.RemoveAt(i);
                        }
                        // Если вдруг ключей не осталось, берём первый ключ у первого потомка.
                        if (node.Keys.Count == 0)
                        {
                            node.Keys.Add(node.Next[0].Keys[0]);
                        }
                        // Выключаем флаг.
                        needToMerge = false;
                    }
                }
                // Если объединение не требуется, при наличии искомого ключа в узле, заменяем его на переданный снизу.
                else
                {
                    if (node.Keys.Find(a => a.Key == key) != null)
                    {
                        node.Keys[node.Keys.FindIndex(a => a.Key == key)] = returns;
                    }
                }
            }
        }
    }
}
