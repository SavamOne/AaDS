using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab2
{
    //Интерфейс для алгоритма сортировки
    //Должен иметь 2 метода, который сортрует текущий массив или возвращает новый отсортированный
    //Также интерфейс ограничивает Т - этот тип данных должен реализовывать интерфейс IComparable,
    //который позволяет сравнивать друг с другом два значения
    public interface ISortable<T> where T : IComparable<T>
    {
        public void SortRef(T[] _arr); 
        public T[] SortVal(T[] _arr);
    }
}
