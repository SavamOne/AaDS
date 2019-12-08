using System;


//Базовый класс дерева
//Содержит объявления основных методов и полей, реализацию методов для вывода дерева, реализацию класса узла дерева
namespace Lab3
{
    public abstract class Tree<TKey, TValue> where TKey: IComparable<TKey>
    {

        //Основной класс для узла дерева
        //Содержит поля для ключа и значения, а так же высоту и ссылки на левого и правого сыновей и ссылку на родителя
        protected class Node
        {
            public Node Left { get; set; }
            public Node Right { get; set; }
            public Node Parent { get; set; }
            public int Height { get; set; }

            public TKey Key { get; set; }

            public TValue Value { get; set; }

            public Node(TKey key, TValue value)
            {
                Key = key;
                Value = value;
                Height = 0;
            }
            public Node(Node parent, TKey key, TValue value) : this(key, value)
            {
                Parent = parent;
                if (parent.Key.CompareTo(Key) > 0)
                    parent.Left = this;
                else
                    parent.Right = this;
            }
        }

        //Корень дерева
        protected Node Root { get; set; }


        //Основные методы, которые нужны для работы с деревом - добавить узел, удалить узел, проверить существование узла по ключу, вернуть значение узла по ключу
        public abstract void Add(TKey key, TValue value);

        public abstract void Delete(TKey key);

        public abstract bool Exists(TKey key);

        public abstract TValue this[TKey key]
        {
            get;
            set;
        }




        //Вывод всех элементов по порядку
        public override string ToString() => InOrder(Root);
        protected string InOrder(Node node)
        {
            var res = "";
            if (node != null)
            {
                res += InOrder(node.Left);
                res += $"[{node.Key}: {node.Value}],";
                res += InOrder(node.Right);
            }
            return res;
        }


        //Вывод всех узлов в том порядке, как они расположены в дереве
        public string Print() => Print(Root);
        protected string Print(Node node)
        {
            var res = "";

            if (node != null)
            {
                if (node.Parent == null)
                    res += $"Root: {node.Key}  : {node.Value}\n";
                else
                {
                    if (node.Parent.Left == node)
                        res += $"Left for {node.Parent.Key}  --> {node.Key}  : {node.Value}\n";

                    if (node.Parent.Right == node)
                        res += $"Right for {node.Parent.Key} --> {node.Key}  : {node.Value}\n";
                }

                if (node.Left != null)
                    res += Print(node.Left);
                if (node.Right != null)
                    res+= Print(node.Right);
            }
            return res;
        }
    }
}
