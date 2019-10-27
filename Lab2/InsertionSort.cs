using System;

namespace Lab2
{
    //Класс для сортировки вставками
    //Реализует базовый класс SortBase
    //Необходим для сортировки подмассивов в TimSort
    public class InsertionSort<T> : SortBase<T> where T : IComparable<T>
    {
        protected override void MainSort()
        {
            for (int i = 1; i < arr.Length; i++)
            {
                T cur = arr[i];
                int j = i;
                while (j > 0 && cur.CompareTo(arr[j - 1]) < 0)
                {
                    arr[j] = arr[j - 1];
                    j--;
                }
                arr[j] = cur;
            }
        }
    }
}
