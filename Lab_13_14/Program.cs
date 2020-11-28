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

                equalityCounter++;
                
                while (j >= 0 && inputArray[j] > temp)
                {
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
                    equalityCounter++;
                    if (inputArray[min] > inputArray[j])
                    {
                        min = j;
                    }
                }

                if (min != i)
                {
                    var temp = inputArray[i];
                    inputArray[i] = inputArray[min];
                    inputArray[min] = temp;
                    moveCounter++;
                }
            }
            
            Console.WriteLine($"Количество сравнений в сортировке выбором: {equalityCounter}");
            Console.WriteLine($"Количество перестановок в сортировке выбором: {moveCounter}");
            
            return inputArray;
        }

        private static int[] advancedInsertionSort(int[] inputArray)
        {
            moveCounter = 0;
            equalityCounter = 0;

            int[] steps;
            int stepsCount;
            
            Console.WriteLine("D or C?");
            if (Console.ReadLine().Equals("C"))
            {
                Console.WriteLine("Введите кол-во шагов");
                if (!int.TryParse(Console.ReadLine(), out stepsCount))
                {
                    Console.WriteLine("Ввод неверен, сортировка не произведена");
                    return inputArray;
                }
                
                steps = new int[stepsCount];
                stepsCount--;
                Console.WriteLine("Введите величины шагов в возрастающем порядке");
                for (int i = 0; i < steps.Length; i++)
                {
                    if (!int.TryParse(Console.ReadLine(), out int s))
                    {
                        Console.WriteLine("Ввод неверен, сортировка не произведена");
                        return inputArray;
                    }

                    steps[i] = s;
                }
            }
            else
            {
                steps = new int[] { 1, 3, 7, 15, 31, 63, 127, 255, 511, 1023, 2047, 4095, 8191, 16383, 32767, 65535, 131071};
                stepsCount =  (int)Math.Log(inputArray.Length, 2) - 1;
            }

            //Console.WriteLine(stepsCount);
            
            for (int i = stepsCount; i >= 0; i--)
            {
                var k = steps[i];
                for (int j = k; j < inputArray.Length; j++)
                {
                    var temp = inputArray[j];
                    var l = j - k;
                    
                    equalityCounter++;
                    
                    while (l >= 0 && temp < inputArray[l])
                    {
                        inputArray[l + k] = inputArray[l];
                        l -= k;
                        moveCounter++;
                    }

                    inputArray[l + k] = temp;
                    
                }
            }
            
            Console.WriteLine($"Количество сравнений в сортировке методом Шелла: {equalityCounter}");
            Console.WriteLine($"Количество перестановок в сортировке методом Шелла: {moveCounter}");

            return inputArray;
        }
        
        private static int[] quickSort(int[] inputArray, int leftStart, int rightStart, ref ulong equalityCounter, ref ulong moveCounter)
        {
            int middle = inputArray[(rightStart + leftStart) / 2];
            int leftSide = leftStart;
            int rightSide = rightStart;

            do
            {
            
                while(inputArray[leftSide] < middle)
                {
                    equalityCounter++;
                    leftSide++;
                }

                while (inputArray[rightSide] > middle)
                {
                    equalityCounter++;
                    rightSide--;
                }

                if (leftSide <= rightSide)
                {
                    equalityCounter++;
                    var temp = inputArray[leftSide];
                    inputArray[leftSide] = inputArray[rightSide];
                    inputArray[rightSide] = temp;
                    moveCounter++;

                    leftSide++;
                    rightSide--;
                }

            } while (leftSide < rightSide);

            if (leftStart < rightSide)
            {
                quickSort(inputArray, leftStart, rightSide, ref equalityCounter, ref moveCounter);
            }

            if (rightStart > leftSide)
            {
                quickSort(inputArray, leftSide, rightStart, ref equalityCounter, ref moveCounter);
            }

            return inputArray;
        }

        private static int[] heapsort(int[] inputArray)
        {
            moveCounter = 0;
            equalityCounter = 0;

            var curr = (inputArray.Length - 1) / 2;

            while (curr >= 0)
            {
                maxHeapify(curr, inputArray, inputArray.Length, ref equalityCounter, ref moveCounter);
                curr--;
            }
            //printArray(inputArray);

            for (int i = inputArray.Length - 1; i > 0; i--)
            {
                var temp = inputArray[0];
                inputArray[0] = inputArray[i];
                inputArray[i] = temp;

                moveCounter++;
                
                maxHeapify(0, inputArray, i, ref equalityCounter, ref moveCounter);
            }
            
            Console.WriteLine($"Количество сравнений в пирамидальной сортировке: {equalityCounter}");
            Console.WriteLine($"Количество перестановок в пирамидальной сортировке: {moveCounter}");
            
            return inputArray;
        }

        private static void maxHeapify(int curr, int[] inputArray, int size, ref ulong ec, ref ulong mc)
        {
            var left = 2 * curr + 1;
            var right = 2 * curr + 2;
            var mem = curr;

            if (right < size)
            {
                if (inputArray[left] > inputArray[right])
                {
                    mem = inputArray[left] > inputArray[curr] ? left : curr;
                    ec++;
                }
                else
                {
                    mem = inputArray[right] > inputArray[curr] ? right : curr;
                    ec++;
                }
            }
            else if (left < size && inputArray[curr] < inputArray[left])
            {
                mem = left;
                ec++;
            }

            if (mem != curr)
            {
                var temp = inputArray[curr];
                inputArray[curr] = inputArray[mem];
                inputArray[mem] = temp;

                mc++;
                
                maxHeapify(mem, inputArray, size, ref ec, ref mc);
            }
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
                case "SHELL":
                    if (array.Length > 0)
                    {
                        int[] arrayCopy = new int[array.Length];
                        Array.Copy(array, arrayCopy, array.Length);
                        var result = advancedInsertionSort(arrayCopy);
                        printArray(result);
                    }
                    break;
                case "QS":
                    if (array.Length > 0)
                    {
                        equalityCounter = 0;
                        moveCounter = 0;
                        
                        int[] arrayCopy = new int[array.Length];
                        Array.Copy(array, arrayCopy, array.Length);
                        var result = quickSort(arrayCopy, 0, arrayCopy.Length - 1, ref equalityCounter, ref moveCounter);
                        
                        Console.WriteLine($"Количество сравнений в быстрой сортировке: {equalityCounter}");
                        Console.WriteLine($"Количество перестановок в быстрой сортировке: {moveCounter}");
                        
                        printArray(result);
                    }
                    break;
                case "HS":
                    if (array.Length > 0)
                    {
                        int[] arrayCopy = new int[array.Length];
                        Array.Copy(array, arrayCopy, array.Length);
                        var result = heapsort(arrayCopy);
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