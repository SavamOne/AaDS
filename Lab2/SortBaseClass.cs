using System;

namespace Lab2
{
    //Базовый класс для алгоритма сортировки
    //Реализует интерфейс ISortable, реализует базоую реализацию методов SortRef и SortVal
    //Объявляет сам массив и "главный" метод для его сортировки 
    public abstract class SortBase<T> : ISortable<T> where T : IComparable<T>
    {
        protected T[] arr;
        virtual public void SortRef(T[] _arr)
        {
            arr = _arr;
            MainSort();
        }
        virtual public T[] SortVal(T[] _arr)
        {
            arr = (T[])_arr.Clone();
            MainSort();
            return arr;
        }

        abstract protected void MainSort();
    }
}
