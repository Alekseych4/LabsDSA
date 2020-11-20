using System;

namespace Lab_13_14
{
    internal class Program
    {
        private static int[] array;
        private static int equalityCounter;
        private static int moveCounter;
        
        public static void Main(string[] args)
        {
            Console.WriteLine("RAN  создать массив со случайными числами");
            Console.WriteLine("PR  вывести исходный массив");
            Console.WriteLine("BUS  сортировка пузырьком");
            Console.WriteLine("INS  сортирока вставками");
            Console.WriteLine("SES  сортировка выбором");
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

        private static int[] randomArray(int num)
        {
            Random random = new Random();
            var array = new int[num];
            
            for (int i = 0; i < num; i++)
            {
                array[i] = random.Next(-100, 1000);
            }

            return array;
        }

        private static int[] bubbleSort(int [] inputArray)
        {
            moveCounter = 0;
            equalityCounter = 0;

            for (int i = inputArray.Length; i > 0; i--)
            {
                for (int j = 0; j < i - 1; j++)
                {
                    equalityCounter++;
                    
                    if (inputArray[j] > inputArray[j + 1])
                    {
                        var swap = inputArray[j + 1];
                        inputArray[j + 1] = inputArray[j];
                        inputArray[j] = swap;
                        moveCounter++;
                    }
                }
                
            }
            Console.WriteLine($"Количество сравнений в сортировке пузырька (обмена): {equalityCounter}");
            Console.WriteLine($"Количество перестановок в сортировке пузырька (обмена): {moveCounter}");

            return inputArray;
        }

        private static int[] insertionSort(int[] inputArray)
        {
            moveCounter = 0;
            equalityCounter = 0;
            
            for (int i = 0; i < inputArray.Length - 1; i++)
            {
                var j = i;
                var temp = inputArray[i + 1];

                
                while (j >= 0 && inputArray[j] > temp)
                {
                    equalityCounter++;
                    inputArray[j + 1] = inputArray[j];
                    j--;
                    moveCounter++;
                }

                inputArray[j + 1] = temp;
            }
            Console.WriteLine($"Количество сравнений в сортировке вставками: {equalityCounter}");
            Console.WriteLine($"Количество перестановок в сортировке вставками: {moveCounter}");

            return inputArray;
        }

        private static int[] selectionSort(int[] inputArray)
        {
            moveCounter = 0;
            equalityCounter = 0;
            
            for (int i = 0; i < inputArray.Length; i++)
            {
                int min = i;
                
                for (int j = i + 1; j < inputArray.Length; j++)
                {
                    if (inputArray[min] > inputArray[j])
                    {
                        equalityCounter++;
                        min = j;
                    }
                }

                var temp = inputArray[i];
                inputArray[i] = inputArray[min];
                inputArray[min] = temp;
                moveCounter++;
            }
            
            Console.WriteLine($"Количество сравнений в сортировке выбором: {equalityCounter}");
            Console.WriteLine($"Количество перестановок в сортировке выбором: {moveCounter}");
            
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
                case "INS":
                    if (array.Length > 0)
                    {
                        int[] arrayCopy = new int[array.Length];
                        Array.Copy(array, arrayCopy, array.Length);
                        var result = insertionSort(arrayCopy);
                        printArray(result);
                    }
                    break;
                case "SES":
                    if (array.Length > 0)
                    {
                        int[] arrayCopy = new int[array.Length];
                        Array.Copy(array, arrayCopy, array.Length);
                        var result = selectionSort(arrayCopy);
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