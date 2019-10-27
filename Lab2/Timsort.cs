using System;
using System.Collections.Generic;

namespace Lab2
{
    //Класс для TimSort
    //Реализует базовый класс SortBase
    //Сортировка состоит из:
    // 1) Поиск MinRun (FindMinrun)
    // 2) Разделение массива на подмассивы (SubArraysFragmentation)
    //  2.1) Поиск run'ов (и его обратная перестановка в случае необходимости)
    //  2.2) Дополнение этого run'а до подмассива необходимым количествов элеметов
    //  2.3) Сортировка вставками, если выполнен 2.2
    //  2.4) Добавление этого подмассива в стек
    // 3) Объединение подмассивов в один отсортированный (MegreDistribution)

    public class TimSort<T> : SortBase<T> where T : IComparable<T>
    {
        enum RunOrder { LowerOrEqual, Higher };

        private Stack<T[]> subArrays;
        private int minrun;
        private int currentIndex;

        public override void SortRef(T[] _arr)
        {
            base.SortRef(_arr);
            Array.Copy(subArrays.Pop(), _arr, _arr.Length);
        }
        public override T[] SortVal(T[] _arr)
        {
            base.SortRef(_arr);
            return subArrays.Pop();
        }

        protected override void MainSort()
        {
            currentIndex = 0;
            subArrays = new Stack<T[]>();

            FindMinrun();
            SubArraysFragmentation();
            MegreDistribution();
        }

        //Поиск оптимального minrun'а
        private void FindMinrun()
        {
            int r = 0, n = arr.Length;
            while (n >= 64)
            {
                r |= n & 1;
                n >>= 1;
            }
            minrun = n + r;
        }

        //Разделение массива на подмассивы 
        private void SubArraysFragmentation()
        {
            do
            {
                int beginIndex = currentIndex;
                int runSize, subArraySize = runSize = RunFragmentation(out RunOrder runOrder); //Поиск run'a и его порядок

                if (runSize < minrun) //Если размер run'a меньше minrun, то дополнить этот массив до размера Minrun (или до скольки возможно)
                {
                    currentIndex = Math.Min(currentIndex + minrun - subArraySize, arr.Length - 1);
                    subArraySize = Math.Min(minrun, arr.Length - beginIndex);
                }

                
                var subArray = new T[subArraySize]; //Создание подмассива и копирование в него элементов
                Array.Copy(arr, beginIndex, subArray, 0, subArraySize);

                if (runOrder == RunOrder.Higher) //Если последовательность упорядочена строго по убыванию, то инвертировать ее
                    InvertSubArr(subArray, runSize);

                if (subArraySize != runSize) //Если размеры подмассива не совпадают с размером run'a, то отсортировать его сортировкой вставками
                    new InsertionSort<T>().SortRef(subArray);

                subArrays.Push(subArray);  //Добавить этот подмассив в стек

            } while (currentIndex < arr.Length - 1); //Делать пока не закончится массив
        }

   
        //Определение порядка run'a и его поиск через метод FindRun
        //Возвращает длину run'а и его порядок сортировки
        private int RunFragmentation(out RunOrder runOrder)
        {
            if (currentIndex >= arr.Length - 1) //Если остался один элемент в массиве, то вернуть размер равный 1
            {
                currentIndex++;
                runOrder = RunOrder.LowerOrEqual;
                return 1;
            }

            runOrder = arr[currentIndex].CompareTo(arr[currentIndex + 1]) <= 0 ? RunOrder.LowerOrEqual : RunOrder.Higher;
            
            int runSize = FindRun(runOrder);

            return runSize;
        }

        //Поиск run'a в определенном упорядоченной последовательности
        //Возвращает длину run'a
        private int FindRun(RunOrder runOrder)
        {
            currentIndex ++;
            int runSize = 2;
            bool isWorks = true;
            do
            {
                if (currentIndex + 1 < arr.Length &&
                    (arr[currentIndex].CompareTo(arr[currentIndex + 1]) <= 0 && runOrder == RunOrder.LowerOrEqual ||
                     arr[currentIndex].CompareTo(arr[currentIndex + 1]) > 0  && runOrder == RunOrder.Higher)
                    )
                {
                    runSize++;
                }
                else
                    isWorks = false;
                currentIndex++;
            } while (isWorks);
            return runSize;
        }

        //Инверсия элементов в подмассиве от 0 до end
        private void InvertSubArr(T[] arr, int end)
        {
            for (int i = 0; i < end / 2; i++)
            {
                T s = arr[i];
                arr [i] = arr[arr.Length - 1 - i];
                arr[arr.Length - 1 - i] = s;
            }
        }

        //Объединение подмассивов в один отсортированный
        //Каждые 2 подмассива объединяются друг с другом через метод Merge и добавляются во временный стек
        //Временный стек становится основным и процедура повторяется, пока не остается 1 подмассив в стеке
        //Если подмассивов в стеке нечетное количество, то один из подмассиов сразу перемещается во временный стек
        //Пример:
        //  A B C D E F G H I => A BC DE FG HI -> A BCDE FGHI -> A BCDEFGHI -> ABCDEFGHI
        private void MegreDistribution()
        {
            Stack<T[]> temp;
       
            while (subArrays.Count > 1)
            {
                temp = new Stack<T[]>();

                if (subArrays.Count % 2 == 1)
                    temp.Push(subArrays.Pop());

                while (subArrays.Count > 1)
                    temp.Push(Merge(subArrays.Pop(), subArrays.Pop()));

                
                subArrays = temp;
            }
        }

        //Объединение 2 отсортированных подмассивов в один отсортированный
        //Каждый верхний элемент одного подмассива сравнивается с верхним элементом другого
        //БОльший элемент из двух добавляется в конец нового массива, указатель на верхний элемент
        //того подмассива, откуда был взят элемент, уменьшается на 1
        //После того, как все элементы из одного подмассива были добавлены в новый массив, в новый массив добавляются
        //оставшиеся элементы из второго подмассива
        private T[] Merge(T[] firstArr, T[] secondArr)
        {
            int curPosFirst = firstArr.Length - 1, curPosSecond = secondArr.Length - 1;
            var tArr = new T[firstArr.Length + secondArr.Length];
            int tArrPos = tArr.Length - 1;

            while(curPosFirst >= 0 && curPosSecond >= 0)
            {
                T a = firstArr[curPosFirst], b = secondArr[curPosSecond];
                if(a.CompareTo(b) > 0)
                {
                    tArr[tArrPos] = a;
                    curPosFirst--;
                }
                else
                {
                    tArr[tArrPos] = b;
                    curPosSecond--;
                }
                tArrPos--;
            }
            while(curPosSecond >= 0)
            {
                tArr[tArrPos] = secondArr[curPosSecond];
                curPosSecond--;
                tArrPos--;
            }
            while(curPosFirst >=0)
            {
                tArr[tArrPos] = firstArr[curPosFirst];
                curPosFirst--;
                tArrPos--;
            }

            return tArr;
        }
    }
}
