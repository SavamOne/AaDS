using System;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Tree<int, string> tree = new AVL_Tree<int, string>();

            tree.Add(100, "100");
            tree.Add(234, "234");
            tree.Add(34, "34");
            tree.Add(1, "1");
            tree.Add(23, "23");
            tree.Add(30, "30");
            tree.Add(56, "56");
            tree.Add(239,"239");
            tree.Add(89,"89");
            tree.Add(32,"32");
            tree.Add(21,"21");
            tree.Add(1111,"1111");


            tree.Delete(34);

            Console.WriteLine(tree);
            Console.WriteLine(tree.Print());

            Console.ReadKey();
        }



    }
}
