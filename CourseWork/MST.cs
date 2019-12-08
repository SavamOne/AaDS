using System;
using System.Collections.Generic;

namespace CourseWork
{
    //Базовый класс остовного дерева
    //Содержит объявления основных методов и полей, реализацию методов для вывода дерева, реализацию класса грани графа
    abstract class MST
    {
        public class Edge : IComparable<Edge>
        {
            public string N1 { get; set; } //Как название для вершины можно использовать любую строку
            public string N2 { get; set; }
            public int Сost { get; set; }

            public Edge(string n1, string n2, int cost)
            {
                N1 = n1;
                N2 = n2;
                Сost = cost;
            }

            public override string ToString() => $"{N1} - {N2} : {Сost}"; //Вывод грани графа
            public int CompareTo(Edge other) => Сost.CompareTo(other.Сost); //Сортировку граней в списке/массиве осуществлять по значению стоимости
        }

        protected List<Edge> Graph { get; set; } //Граф - содержит все грани, которые добавил пользователь
        protected List<Edge> Tree { get; set; } //Дерево - содержит только те грани, которые подходят для остовного дерева
        public int Count => Tree.Count; //Количество элементов в дереве

        public MST()
        {
            Graph = new List<Edge>();
            Tree = new List<Edge>();
        }

        public abstract void AddEdge(string n1, string n2, int cost); //Добавить грань
        public abstract void FindMST(); //Найти остовное дерево

        public IEnumerator<Edge> GetEnumerator() => Tree.GetEnumerator();
        public string this[int key] => Tree[key].ToString();
    }
}
