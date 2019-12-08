using System;
using System.Collections.Generic;
using Lab2;

namespace CourseWork
{
    //Реализация алгоритма Курскала, наследуется от класса остовного дерева
    class KruskalMST : MST
    {
        private List<string> Vertexs { get; set; } //Список всех вершин в графе
        public KruskalMST() : base() => Vertexs = new List<string>();

        private bool ContainsVertexsPair(string n1, string n2) //Проверить существование такой графни в графе, проверяется так же грани с переставленными вершинами (A, B и  B,A - одна и та же вершина)
        {
            foreach(Edge edge in Graph)
            {
                if ((edge.N1 == n1 && edge.N2 == n2) || (edge.N2 == n1 && edge.N1 == n2))
                    return true;
            }
            return false;
        }

        public override void AddEdge(string n1, string n2, int cost) //Добавить грань в список
        {
            if(!ContainsVertexsPair(n1, n2)) //Если такой графни не существует
            {
                Graph.Add(new Edge(n1, n2, cost)); //То добавить ее в список граней графа
                if (!Vertexs.Contains(n1)) //Если таких вершин не было, то добавить их в список вершин
                    Vertexs.Add(n1);
                if (!Vertexs.Contains(n2))
                    Vertexs.Add(n2);
            }
            else
                throw new Exception("Такая комбинация вершин уже существует");
        }

        public override void FindMST() //Поиск остовного дерева
        {
            Tree.Clear(); //Очистить дерево, если до этого остовное дерево искалось

            var GraphArr = Graph.ToArray();
            new TimSort<Edge>().SortRef(GraphArr); //Отсортировать список граней графа собственной реализацией TimSort (ЛР 2)

            var VertexsUnion = new DSU<string>(Vertexs); //Создать непересекающиеся множества из списка вершин

            foreach (Edge edge in GraphArr) //Для каждой грани в отсортированном массиве граней графа
            {
                string n1 = VertexsUnion.Find(edge.N1); //Найти родителя вершины 1 и 2 
                string n2 = VertexsUnion.Find(edge.N2);
                if (n1 != n2) //Если они различны, то добавить эту грань в дерево + объединить в одно множество два множества вершин
                {
                    Tree.Add(edge);
                    VertexsUnion.Unite(n1, n2);
                }
            }
        }
    }
}
