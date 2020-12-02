using System;

namespace Lab_16
{
    internal class Program
    {
        private static string[] keys = new[] {"begin;", "!end", "do", "start", "for", "else", "as", "var", "IF", "integer"};
        private static int tableLength;
        private static string[] hashTable;
        private static long equalityCounter;
        private static long totalOperations;
        public static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("Введите длину хэш-таблицы");
                Console.WriteLine();
                var lengthStr = Console.ReadLine();
                if (int.TryParse(lengthStr, out var length))
                {
                    if (length > 0)
                    {
                        tableLength = length;
                        hashTable = new string[length];
                        break;
                    }
                }
            } while (true);
            
            Console.WriteLine("L  ввести длину хэш-таблицы");
            Console.WriteLine("A  добавить элемент");
            Console.WriteLine("F  поиск элемента в хэш-таблице");
            Console.WriteLine("C  хэш-код слова");
            Console.WriteLine("P  вывести на экран");
            Console.WriteLine("TO  количество всех сравнений за время использования");
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

        private static int hash(string key)
        {
            var charArray = key.ToCharArray();
            int sum = 0;
            
            for (int i = 0; i < charArray.Length; i++)
            {
                sum += charArray[i];
            }
            
            //Console.WriteLine($"{key} = {sum}");

            return sum % tableLength;
        }

        private static bool putInTable(int hashCode, string el)
        {
            equalityCounter = 0;
            equalityCounter++;
            if (string.IsNullOrEmpty(hashTable[hashCode]))
            {
                hashTable[hashCode] = el;
                return true;
            }

            equalityCounter++;
            if (hashTable[hashCode].Equals(el))
            {
                return false;
            }

            for (int k = 1; k < tableLength; k++)
            {
                var j = (hashCode + k) % tableLength;
                
                equalityCounter++;
                if (string.IsNullOrEmpty(hashTable[j]))
                {
                    hashTable[j] = el;
                    return true;
                }
                else if (hashTable[hashCode].Equals(el))
                {
                    equalityCounter++;
                    return false;
                }
            }
            
            return false;
        }

        private static void printTable()
        {
            for (int i = 0; i < hashTable.Length; i++)
            {
                Console.Write($"[{i}]: {hashTable[i]}  ");
            }
            Console.WriteLine();
        }

        private static bool searchKey(string key)
        {
            equalityCounter = 0;
            var hashSearch = hash(key);
            equalityCounter++;
            if (hashTable[hashSearch].Equals(key))
            {
                Console.WriteLine($"Элемент находится в {hashSearch} ячейке");
                return true;
            }
            
            for (int k = 1; k < tableLength; k++)
            {
                var j = (hashSearch + k) % tableLength;
            
                equalityCounter++;
                if (hashTable[j] != null && hashTable[j].Equals(key))
                {
                    Console.WriteLine($"Элемент находится в {j} ячейке");
                    return true;
                }
            }

            return false;
        }

        private static void commandRecognizer(string command)
        {
            switch (command)
            {
                case "L":
                    do
                    {
                        Console.WriteLine("Введите длину хэш-таблицы");
                        var lengthStr = Console.ReadLine();
                        if (int.TryParse(lengthStr, out var length))
                        {
                            if (length > 0)
                            {
                                totalOperations = 0;
                                equalityCounter = 0;
                                tableLength = length;
                                hashTable = new string[length];
                                break;
                            }
                        }
                    } while (true);
                    break;
                case "A":
                    Console.WriteLine("Введите элемент:");
                    var e = Console.ReadLine();

                    var code = hash(e);
                    if (putInTable(code, e))
                    {
                        Console.WriteLine("Элемент успешно добавлен");
                        totalOperations += equalityCounter;
                    }
                    else
                    {
                        Console.WriteLine("Элемент не добавлен, таблица заполнена или такой ключ уже существует");
                    }
                    
                    Console.WriteLine($"Кол-во сравнений: {equalityCounter}");
                    
                    break;
                case "F":
                    Console.WriteLine("Введите ключ для поиска");
                    var keyForSearch = Console.ReadLine();
                    if (!searchKey(keyForSearch))
                    {
                        Console.WriteLine("Данный элемент отсутствует в хэш-таблице");
                    }
                    
                    Console.WriteLine($"Кол-во сравнений: {equalityCounter}");
                    totalOperations += equalityCounter;
                    break;
                case "C":
                    var input = Console.ReadLine();
                    var h = hash(input);
                    Console.WriteLine($"Хэш-код значения {input}: {h}");
                    break;
                case "P":
                    printTable();
                    break;
                case "TO":
                    Console.WriteLine("Все сравнения: " + totalOperations);
                    break;
            }
        }
    }
}