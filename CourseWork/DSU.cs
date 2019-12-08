using System;
using System.Collections.Generic;
using Lab3;


namespace CourseWork
{
    //Класс реализующий систему непересекающихся множества
    class DSU<T> where T : IComparable<T>
    {
        AVL_Tree<T, T> Items { get; set; } //Словарь элементов (Используется собственная реализация АВЛ-дерева (ЛР 3)

        public DSU() => Items = new AVL_Tree<T, T>(); //Инициализация словаря

        public DSU(List<T> list) : this() //Инициализация словаря и его заполнение элементами списка методом Add
        {
            foreach (T elem in list)
                Add(elem);
        }

        public void Add(T item) => Items.Add(item, item); //Добавить элемент в словарь, где у элемента ключ и значение будут одинаковыми

        public T Find(T item) //Поиск "родителя" элемента
        {
            if (Items[item].CompareTo(item) == 0) //Если ключ и значение у элемента одинаковые, значит он и является родителем
                return item;
            return Find(Items[item]); //Иначе вызвать поиск родителя значения элемента
        }

        public void Unite(T what, T where) //Объединение 2 элементов 
        {
            what = Find(what); //Поиск родителей двух элементов
            where = Find(where);
            Items[what] = where; //Присвоить одному из родителей другого родителя как значение
        }

        public bool Contains(T item) => Items.Exists(item); //Проверка на существование элемента в СНМ
    }
}
