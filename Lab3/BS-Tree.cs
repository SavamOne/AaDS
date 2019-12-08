using System;

namespace Lab3
{

    //Реализация бинарного дерева поиска, наследуется от базового класса дерева
    public class BS_Tree<TKey, TValue> : Tree<TKey, TValue> where TKey : IComparable<TKey>
    {

        //Добавление узла - если узлов нет, то он будет являться корнем, если есть, то проход в нужном порядке по всем сыновьям и добавление к нему левого или правого сына
        public override void Add(TKey key, TValue value)
        {
            if (Exists(key))
                throw new Exception("Такой ключ уже существует");

            if (Root == null)
            {
                Root = new Node(key, value);
                return;
            }

            Node x = Root, xParent = null;
            while (x != null)
            {
                xParent = x;
                x = x.Key.CompareTo(key) > 0 ? x.Left : x.Right;
            }
            x = new Node(xParent, key, value);

            CalculateHeight(x);
        }


        //Поиск максимального ключа в дереве => самый правый сын
        protected Node Maximum(Node x)
        {
            while (x.Right != null)
                x = x.Right;
            return x;
        }


        //Расчет высоты для узла Х и всех родителей вверх
        protected virtual void CalculateHeight(Node parent)
        {
            Node x = parent;
            while (x != null)
            {
                x.Height = Math.Max(x.Left?.Height ?? -1, x.Right?.Height ?? -1) + 1;
                x = x.Parent;
            }
        }

        //Поиск узла по ключу - проход по всем нужным элеметам дерева в поисках нужного ключа
        protected virtual Node Find(TKey key)
        {
            Node x = Root;
            while (x != null)
            {
                if (x.Key.CompareTo(key) == 0)
                    return x;

                x = x.Key.CompareTo(key) > 0 ? x.Left : x.Right;
            }

            return null;
        }


        //Значение по ключу - Поиск узла по ключу и возврат/замена значения
        public override TValue this[TKey key]
        {
            get
            {
                Node x = Find(key);
                if (x != null)
                    return x.Value;
                throw new Exception("Такой ключ не существует");
            }
            set
            {
                Node x = Find(key);
                if (x != null)
                    x.Value = value;
                else
                    throw new Exception("Такой ключ не существует");
            }
        }
           

        //Существование узла - поиск узла по ключу и возврат true, если такой узел не равен Null
        public override bool Exists(TKey key) => Find(key) != null;

        public override void Delete(TKey value) => Delete(Find(value));

        //Удаление узла  - поиск узла, если детей нет - вызов функции DeleteZeroChilds, если хотя бы один - DeleteOneChild, если есть оба - DeleteTwoChilds
        protected virtual void Delete(Node del)
        {
            if (del == null)
                throw new Exception("Такой ключ не существует");

            if (del.Left == null && del.Right == null)
                DeleteZeroChilds(del);
            else if (del.Left == null || del.Right == null)
                DeleteOneChild(del);
            else
                DeleteTwoChilds(del);
        }

        //Если детей нет - убрать ссылку на сына у его родителя
        protected virtual void DeleteZeroChilds(Node del)
        {
            if (del == Root)
            {
                Root = null;
                return;
            }

            Node parent = del.Parent;
            if (parent.Left == del)
                parent.Left = null;
            else if (parent.Right == del)
                parent.Right = null;

            CalculateHeight(parent);
        }


        //Если один сын - ссылку на сына у его родителя заменить ссылкой на сына удаляемого узла, ссылку на родителя сына удаляемого узла заменить ссылкой на родителя удаляемого узла
        protected virtual void DeleteOneChild(Node del)
        {
            if (del == Root)
            {
                Root = del.Left ?? del.Right;
                Root.Parent = null;

                return;
            }


            Node parent = del.Parent;
            if (del.Left == null)
            {
                if (parent.Left == del)
                    parent.Left = del.Right;
                else
                    parent.Right = del.Right;
                del.Right.Parent = parent;
            }
            else
            {
                if (parent.Left == del)
                    parent.Left = del.Left;
                else
                    parent.Right = del.Left;
                del.Left.Parent = parent;
            }
            CalculateHeight(parent);
        }


        //Если есть оба сына - найти максимальный узел в поддереве левого сына, удалить этот узел и заменить ключ и значение удаляемого узла на значения этого макисмального элемента
        protected virtual void DeleteTwoChilds(Node del)
        {
            Node max = Maximum(del.Left);
            Delete(max);
            del.Key = max.Key;
            del.Value = max.Value;
        }
    }
}
