using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeWorkLevelUp11202004
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            int[] array = new int[] {12, 15};
            Console.WriteLine(SumOfDivided(array));

            return; // всё-таки писать или нет ? :)
        }

        public static string SumOfDivided(int[] lst)
        {
            // массив с простыми делителями (без повторов) всех чисел из нашего списка
            var dividers = new List<int>();

            foreach (var element in lst)
            {
                // беру рассматриваемое число, копирую, при этом по модулю (так как там в отрицательных деление с остатком стрёмное может быть)
                var numeric = (element > 0 ? element : (-1) * element);

                //рассматриваю впотенциально возможные делители этого числа 
                for (var divider = 2; divider * divider <= numeric; divider++)
                {
                    // нашёл делитель 
                    if (numeric % divider == 0)
                    {
                        // сразу сокращаю по максимуму на него
                        while (numeric % divider == 0 && numeric >= divider)
                            numeric /= divider;

                        // проверяю бинарным поиском, есть ли он уже в списке уникальных делителей
                        if (findNumber(dividers, divider) == false)
                            insertToList(dividers, divider); // вставляю его (не нарушая порядок сортировки)
                    }
                }

                // если осталось что-то в числе
                if (numeric != 1)
                    if (findNumber(dividers, numeric) == false) // то же самое: вставляем
                        insertToList(dividers, numeric);
            }

            string answer = "";
            foreach (var divider in dividers)
                answer += ("("
                           + Convert.ToString(divider)
                           + " "
                           + Convert.ToString(lst.Where(element => element % divider == 0).Sum()) 
                           + ")");
            // подсчёт непосрественно второго параметра в выводимой скобке (там где LINQ)
            // тут сразу заюзал LINQ синтаксис, потому что как иначе
            
            return answer;
        }

        // функция бинарного поиска для проверки присутсвия элемента в массиве
        public static bool findNumber(List<int> array, int number)
        {
            var left = 0;
            var right = array.Count - 1;
            while (left <= right)
            {
                int middle = (left + right) / 2;

                if (array[middle] == number)
                    return true;

                if (number < array[middle])
                    right = middle - 1;
                else if (number > array[middle])
                    left = middle + 1;
            }

            return false;
        }

        // функция вставки в массив элемента (в такую позицию, чтобы сортировка не нарушалась.. Чтобы не сортировать каждый раз)
        public static void insertToList(List<int> array, int value)
        {
            var counter = 0;
            while (counter < array.Count && array[counter] < value)
                counter++;
            if (counter >= array.Count)
                array.Add(value);
            else
                array.Insert(counter, value);

            return;
        }
    }
}