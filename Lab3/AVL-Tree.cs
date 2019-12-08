using System;
using System.Diagnostics;

namespace Lab3
{

    //Реализация AVL-дерева, наследуется от класса бинарого дерева поиска
    public class AVL_Tree<TKey, TValue> : BS_Tree<TKey, TValue> where TKey : IComparable<TKey>
    {

        //Баланс - разница в высотах левого сына и правого сына
        private int Balance(Node subtree)
        {
            if (subtree == null)
                return -1;
            return (subtree.Left?.Height ?? -1) - (subtree.Right?.Height ?? -1);
        }
        


        //Метод для балансировки дерева
        private Node BalanceTree(Node node)
        {
            Debug.WriteLine("Баланс");

            Node newNode = node;

            int balance = Balance(node);

            if (balance > 1 && Balance(node.Left) > 0) //Если баланс 2 и баланс левого сына положителен, то делать правый поворот
               newNode = RRotate(node);
            else if (balance > 1 && Balance(node.Left) <= 0)  //Если баланс 2 и баланс левого сына отрицателен, то делать лево-правый поворот
                newNode = LRRotate(node);
            else if (balance < -1 && Balance(node.Right) < 0) //Если баланс -2 и баланс правого сына отрицателен, то делать левый поворот
                newNode = LRotate(node);
            else if (balance < -1 && Balance(node.Right) >= 0) //Если баланс -2 и баланс левого сына положителен, то делать право-левый поворот
                newNode = RLRotate(node);
            

            if (node == Root) //если узлом для балансировки был корень, то сделать корнем отбалансированный узел
                Root = newNode;

            return newNode;
        }


        //Переопределение метода расчета высоты, отличается от родительского тем, что идет проверка баланска на каждом узле и соотвествующая балансировка дерева, если баланс нарушен
        protected override void CalculateHeight(Node node)
        {
            Node x = node;
            while (x != null)
            {
                x.Height = Math.Max(x.Left?.Height ?? -1, x.Right?.Height ?? -1) + 1;
                if (Math.Abs(Balance(x)) > 1)
                    x = BalanceTree(x);
                x = x.Parent;
            }
        }

        //Правый поворот
        //           (A)                        
        //          /   \                           B 
        //        B       G                       /   \  
        //      /   \               =>          C      (A)
        //    C       F                       /   \    /  \ 
        //  /  \                            D      E  F    G
        //D      E
        private Node RRotate(Node node)
        {
            Node left = node.Left;

            node.Left = left.Right;
            left.Right = node;

            left.Parent = node.Parent;
            node.Parent = left;
            if (node.Left != null)
                node.Left.Parent = node;

            if (left.Parent != null)
                if (left.Parent.Right == node)
                    left.Parent.Right = left;
                else
                    left.Parent.Left = left;

            node.Height = Math.Max(node.Left?.Height ?? -1, node.Right?.Height ?? -1) + 1;
            left.Height = Math.Max(left.Left?.Height ?? -1, node.Height) + 1;

            return left;
        }

        //Левый поворот
        //           (A)                       
        //          /   \                          B 
        //        G       B                      /   \  
        //              /   \       =>        (A)      C
        //            F       C              /   \    /  \ 
        //                  /   \          G      F  E    D
        //                E       D
        private Node LRotate(Node node)
        {
            Node right = node.Right;

            node.Right = right.Left;
            right.Left = node;

            right.Parent = node.Parent;
            node.Parent = right;
            if (node.Right != null)
                node.Right.Parent = node;

            if (right.Parent != null)
                if (right.Parent.Right == node)
                    right.Parent.Right = right;
                else
                    right.Parent.Left = right;

            node.Height = Math.Max(node.Left?.Height ?? -1, node.Right?.Height ?? -1) + 1;
            right.Height = Math.Max(right.Right?.Height ?? -1, node.Height) + 1;

            return right;
        }


        //Право-левый поворот
        //                
        //      A                     ((A))
        //    /   \                    /  \                        C
        //   G     (B)      =>       G      C        =>         /     \
        //        /   \                   /  \              ((A))       B
        //      C       F               D     (B)           /   \     /   \
        //    /   \                           / \          G     D   E     F
        //   D     E                         E    F
        private Node RLRotate(Node node)
        {
            node.Right = RRotate(node.Right);
            return LRotate(node);
        }


        //Лево-правый поворот
        //                
        //             A                        ((A))        
        //           /   \                      /   \                    C 
        //         (В)     G     =>            C     G      =>         /   \  
        //        /   \                      /   \                   B      ((A))
        //       F     C                   (B)    D                /   \     /  \ 
        //           /   \                /   \                  F      E   D     G
        //          E     D              F     E     
        private Node LRRotate(Node node)
        {
            node.Left = LRotate(node.Left);
            return RRotate(node);
        }

    }

}
