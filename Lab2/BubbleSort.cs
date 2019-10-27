using System;

namespace Lab2
{
    //Класс для сортировки пузырьком
    //Реализует базовый класс SortBase
    //Нужен просто для сравнения скорости работы алгоритмов сортировки
    class BubbleSort<T> : SortBase<T> where T : IComparable<T>
    {
        protected override void MainSort()
        {
            T temp;
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[i].CompareTo(arr[j]) > 0)
                    {
                        temp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = temp;
                    }
                }
            }
        }
    }
}
