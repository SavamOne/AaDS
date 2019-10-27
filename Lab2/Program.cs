using System;
using System.Diagnostics;

namespace Lab2
{
    class Program
    {
        delegate void SortArray(int[] arr);

        static void SortringMethodPerformance(SortArray sortArrayMethod, int[] arr)
        {
            //клонирование массива
            var newArr = (int[])arr.Clone();

            //инициализация секундометра
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //сортировка
            sortArrayMethod(newArr);

            //остановка секундометра
            stopwatch.Stop();
            
            //Вывод результата
            TimeSpan ts = stopwatch.Elapsed;
            string targetAndMethodNames = (sortArrayMethod.Target != null ? sortArrayMethod.Target?.GetType().Name + "." : "")
                                         + sortArrayMethod.Method.Name;
            Console.WriteLine("Runtime of {0}: {1:00}:{2:00}:{3:00}.{4:00}", targetAndMethodNames, ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
        }
        
        static void Main(string[] args)
        {
            Random rnd = new Random();

            //Демонстрация работы сортировки
            int[] arr = new int[100];
            for (int i = 0; i < arr.Length; i++)
                arr[i] = rnd.Next(0, 101);

            Array.ForEach(arr, elem => Console.Write("{0} ", elem));
            Console.WriteLine();

            ISortable<int> sort = new TimSort<int>();
            sort.SortRef(arr);

            Array.ForEach(arr, elem => Console.Write("{0} ", elem));
            Console.WriteLine();


            Console.WriteLine("--------");


            //Сравнение времени работы различных сортировок (стандартный алгоритм в C#, моя реализация TimSort, Сортировка вствками, Сортировка Пузырьком)
            int[] arr2 = new int[100000];
            for (int i = 0; i < arr2.Length; i++)
                arr2[i] = rnd.Next(0, 101);

            SortringMethodPerformance(Array.Sort, arr2);
            SortringMethodPerformance(new TimSort<int>().SortRef, arr2);
            SortringMethodPerformance(new InsertionSort<int>().SortRef, arr2);
            SortringMethodPerformance(new BubbleSort<int>().SortRef, arr2);


            Console.ReadKey();
        }
    }
}
