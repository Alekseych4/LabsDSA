using System;
using Lab1;

namespace Lab_13_14
{
    internal class Program
    {
        private static int[] array;
        private static ulong equalityCounter;
        private static ulong moveCounter;

        public static void Main(string[] args)
        {
            Console.WriteLine("RAN  создать массив со случайными числами");
            Console.WriteLine("PR  вывести исходный массив");
            Console.WriteLine("BUS  сортировка пузырьком");
            Console.WriteLine("INS  сортирока вставками");
            Console.WriteLine("SES  сортировка выбором");
            Console.WriteLine("SHELL  метод Шелла");
            Console.WriteLine("QS  метод быстрой сортировки");
            Console.WriteLine("HS  пирамидальная сортировка");
            Console.WriteLine("EXT  выход");

            while (true)
            {
                var command = Console.ReadLine();
                if (command.Equals("EXT"))
                {
                    break;
                }

                commandRecognizer(command);
            }
        }

        private static int[] randomArray(int num, int minInclusive, int maxExclusive)
        {
            Random random = new Random();
            var array = new int[num];
            
            for (int i = 0; i < num; i++)
            {
                array[i] = random.Next(minInclusive, maxExclusive);
            }

            return array;
        }

        private static int[] arrayForPocketSort(int n)
        {
            var key = 0;
            var array = new int[n];
            
            for (int i = n - 1; i >= 0; i--)
            {
                array[i] = key;
                key++;
            }

            return array;
        }

        private static int[] sameKeysAndIndices(int[] inputArray)
        {
            var newArray = new int[inputArray.Length];
            for (int i = 0; i < inputArray.Length; i++)
            {
                newArray[inputArray[i]] = inputArray[i];
            }

            return newArray;
        }

        private static int[] sameArray(int[] inputArray)
        {
            for (int i = 0; i < inputArray.Length; i++)
            {
                var key = inputArray[i];
                var temp = inputArray[key];
                inputArray[key] = key;
                inputArray[i] = temp;

                if (inputArray[i] != i)
                {
                    i--;
                }
            }

            return inputArray;
        }
        
        public struct Item
        {
            public int firstKey;
            public int lastKey;
        }

        private static int[] bucketSort(int[] inputArray)
        {
            var arr = new MyDynamicList<int>[inputArray.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                var key = inputArray[i];
                
                if (arr[key] == null)
                {
                    arr[key] = new MyDynamicList<int>();
                    arr[key].add(inputArray[i]);
                }
                else
                {
                    arr[key].add(inputArray[i]);
                }
            }

            for (int i = 0; i < arr.Length; i++)
            {
                inputArray[i] = arr[i].remove()
            }
            return inputArray;
        }

        private static void commandRecognizer(string command)
        {
            switch (command)
            {
                case "RAN":
                    Console.WriteLine("Введите число элементов в массиве:");
                    if (int.TryParse(Console.ReadLine(), out var n))
                    {
                        array = randomArray(n);
                        Console.WriteLine("Массив создан");
                    }
                    else
                    {
                        Console.WriteLine("Введите число!");
                    }
                    
                    break;
                case "PR":
                    if (array.Length > 0)
                    {
                        printArray(array);
                    }
                    break;
                case "BUS":
                    if (array.Length > 0)
                    {
                        int[] arrayCopy = new int[array.Length];
                        Array.Copy(array, arrayCopy, array.Length);
                        var result = bubbleSort(arrayCopy);
                        printArray(result);
                    }
                    break;
                
            }
        }

        private static void printArray(int [] arr)
        {
            foreach (var el in arr)
            {
                Console.Write($"{el}   ");
            }
            Console.WriteLine();
        }
    }
}