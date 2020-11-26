using System;

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
            Console.WriteLine("SKIS  простейшая карманная сортировка со вторым массивом");
            Console.WriteLine("TAS  простейшая карманная сортировка без второго массива");
            Console.WriteLine("BS  обобщенная карманная сортировка");
            Console.WriteLine("RS  поразрядная сортировка");
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
            equalityCounter = 0;
            moveCounter = 0;
            
            var newArray = new int[inputArray.Length];
            for (int i = 0; i < inputArray.Length; i++)
            {
                newArray[inputArray[i]] = inputArray[i];
                moveCounter++;
            }
            
            Console.WriteLine($"Количество перестановок в простейшей карманной сортировке со вторым массивом: {moveCounter}");
            Console.WriteLine($"Количество перестановок в простейшей карманной сортировке со вторым массивом: {equalityCounter}");

            return newArray;
        }

        private static int[] sameArray(int[] inputArray)
        {
            equalityCounter = 0;
            moveCounter = 0;
            
            for (int i = 0; i < inputArray.Length; i++)
            {
                var key = inputArray[i];
                var temp = inputArray[key];
                inputArray[key] = key;
                inputArray[i] = temp;

                moveCounter++;
                equalityCounter++;

                if (inputArray[i] != i)
                {
                    i--;
                }
            }
            
            Console.WriteLine($"Количество перестановок в простейшей карманной сортировке со одним массивом: {moveCounter}");
            Console.WriteLine($"Количество перестановок в простейшей карманной сортировке со одним массивом: {equalityCounter}");

            return inputArray;
        }

        private static int[] bucketSort(int[] inputArray)
        {
            var arr = new MyDynamicQueue<int>[inputArray.Length];
            for (int i = 0; i < inputArray.Length; i++)
            {
                var key = inputArray[i];
                
                if (arr[key] == null)
                {
                    arr[key] = new MyDynamicQueue<int>();
                    arr[key].offer(inputArray[i]);
                }
                else
                {
                    arr[key].offer(inputArray[i]);
                }
            }

            var n = 0;
            var k = 0;

            while (n < inputArray.Length)
            {
                if (arr[k] != null)
                {
                    if (!arr[k].isEmpty())
                    {
                        inputArray[n] = arr[k].remove();
                        n++;
                        continue;
                    }
                }

                k++;
            }
            
            return inputArray;
        }

        private static int[] radixSort(int[] inputArray, int digits)
        {
            var arr = new MyDynamicQueue<int>[inputArray.Length];
            var digit = 1;

            for (int i = 0; i < digits; i++)
            {
                for (int j = 0; j < inputArray.Length; j++)
                {
                    var t = inputArray[j] / digit;
                    var key = t % 10;
                    
                    if (arr[key] == null)
                    {
                        arr[key] = new MyDynamicQueue<int>();
                        arr[key].offer(inputArray[j]);
                    }
                    else
                    {
                        arr[key].offer(inputArray[j]);
                    }
                }
                
                var n = 0;
                var k = 0;

                while (n < inputArray.Length)
                {
                    if (arr[k] != null)
                    {
                        if (!arr[k].isEmpty())
                        {
                            inputArray[n] = arr[k].remove();
                            n++;
                            continue;
                        }
                    }

                    k++;
                }

                digit *= 10;
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
                        array = randomArray(n, 0, n);
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
                case "SKIS":
                    if (array.Length > 0)
                    {
                        var ar = arrayForPocketSort(array.Length);
                        var result = sameKeysAndIndices(ar);
                        printArray(result);
                    }
                    break;
                case "TAS":
                    if (array.Length > 0)
                    {
                        var ar = arrayForPocketSort(array.Length);
                        var result = sameArray(ar);
                        printArray(result);
                    }
                    break;
                case "BS":
                    if (array.Length > 0)
                    {
                        var copyArray = new int[array.Length];
                        Array.Copy(array, copyArray, array.Length);
                        var result = bucketSort(copyArray);
                        printArray(result);
                    }
                    break;
                case "RS":
                    if (array.Length > 0)
                    {
                        var copyArray = new int[array.Length];
                        Array.Copy(array, copyArray, array.Length);
                        
                        var maxNum = array.Length - 1;
                        var countDigits = 0;
                        while (maxNum > 0)
                        {
                            maxNum /= 10;
                            countDigits++;
                        }
                        
                        var result = radixSort(copyArray, countDigits);
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